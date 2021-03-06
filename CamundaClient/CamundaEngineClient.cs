﻿using System;
using System.Collections.Generic;
using System.Linq;
using CamundaClient.Service;
using CamundaClient.Worker;
using CamundaClient.Dto;
using System.Reflection;

namespace CamundaClient
{

    public class CamundaEngineClient
    {
        public static string DEFAULT_URL = "http://localhost:8080/engine-rest/engine/default/";
        public static string COCKPIT_URL = "http://localhost:8080/camunda/app/cockpit/default/";

        private IList<ExternalTaskWorker> _workers = new List<ExternalTaskWorker>();
        private IDictionary<string, HumanTask> _userTasks = new Dictionary<string, HumanTask>();
        private CamundaClientHelper _camundaClientHelper;
        private HumanTaskWorker _humanTaskWorker;
        private EventTaskWorker _eventTaskWorker;

        public CamundaEngineClient(string externalSolution = "") : this(new Uri(DEFAULT_URL), null, null, externalSolution) { }

        public CamundaEngineClient(Uri restUrl, string userName, string password, string externalSolution = "")
        {
            _camundaClientHelper = new CamundaClientHelper(restUrl, userName, password);
            this.Startup(externalSolution);
        }

        public BpmnWorkflowService BpmnWorkflowService => new BpmnWorkflowService(_camundaClientHelper, _userTasks, _workers);

        public HumanTaskService HumanTaskService => new HumanTaskService(_camundaClientHelper, _workers);

        public RepositoryService RepositoryService => new RepositoryService(_camundaClientHelper);

        public ExternalTaskService ExternalTaskService => new ExternalTaskService(_camundaClientHelper);

        public ProcessInstanceService ProcessInstanceService => new ProcessInstanceService(_camundaClientHelper);

        public void Startup(string externalSolution = "")
        {
            //this.ControlTasks();
            this.StartWorkers(externalSolution);
            this.CleanEvents();
            this.RepositoryService.AutoDeploy();
            this.ResetFailedExternalTasks();
        }

        public void Shutdown()
        {
            this.StopWorkers();
        }

        public void StartWorkers(string externalSolution = "")
        {
            try
            {
                Assembly assembly = null;
                if (string.IsNullOrEmpty(externalSolution))
                {
                    assembly = Assembly.GetEntryAssembly();
                }
                else
                {
                    assembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains(externalSolution)).SingleOrDefault();
                }

                var externalTaskWorkers = RetrieveExternalTaskWorkerInfo(assembly);

                foreach (var taskWorkerInfo in externalTaskWorkers)
                {
                    Console.WriteLine($"Register Task Worker for Topic '{taskWorkerInfo.TopicName}'");
                    ExternalTaskWorker worker = new ExternalTaskWorker(ExternalTaskService, taskWorkerInfo);
                    _workers.Add(worker);
                    worker.StartWork();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ControlTasks()
        {
            _humanTaskWorker = new HumanTaskWorker(() => {
                var tasks = this.HumanTaskService.LoadTasks();
                _userTasks = tasks.ToDictionary(task => task.ProcessInstanceId??(Guid.NewGuid().ToString()), task => task);
            });

            _humanTaskWorker.Start();
        }

        public void CleanEvents()
        {
            _eventTaskWorker = new EventTaskWorker(ProcessInstanceService, _workers);
            _eventTaskWorker.Start();
        }

        private static IEnumerable<Dto.ExternalTaskWorkerInfo> RetrieveExternalTaskWorkerInfo(System.Reflection.Assembly assembly)
        {
            // find all classes with CustomAttribute [ExternalTask("name")]
            var a = assembly.GetTypes();
            var externalTaskWorkers =
                from t in assembly.GetTypes()
                let externalTaskTopicAttribute = t.GetCustomAttributes(typeof(ExternalTaskTopicAttribute), true).FirstOrDefault() as ExternalTaskTopicAttribute
                let externalTaskVariableRequirements = t.GetCustomAttributes(typeof(ExternalTaskVariableRequirementsAttribute), true).FirstOrDefault() as ExternalTaskVariableRequirementsAttribute
                where externalTaskTopicAttribute != null
                select new Dto.ExternalTaskWorkerInfo
                {
                    Type = t,
                    TopicName = externalTaskTopicAttribute.TopicName,
                    Retries = externalTaskTopicAttribute.Retries,
                    RetryTimeout = externalTaskTopicAttribute.RetryTimeout,
                    VariablesToFetch = externalTaskVariableRequirements?.VariablesToFetch,
                    TaskAdapter = t.GetConstructor(Type.EmptyTypes)?.Invoke(null) as ExternalTaskAdapter
                };
            return externalTaskWorkers;
        }

        public void StopWorkers()
        {
            if (_humanTaskWorker != null)
            {
                _humanTaskWorker.Stop();
            }

            if (_eventTaskWorker != null)
            {
                _eventTaskWorker.Stop();
            }

            foreach (ExternalTaskWorker worker in _workers)
            {
                worker.StopWork();
            }
        }

        public void ResetFailedExternalTasks()
        {
            var tasks = this.ExternalTaskService.GetExternalTasksNoRetries();
            if (tasks != null && tasks.Count > 0)
            {
                foreach (var task in tasks)
                {
                    this.ExternalTaskService.SetExternalTaskRetries(task.Id);
                }
            }
        }

        // HELPER METHODS

    }
}