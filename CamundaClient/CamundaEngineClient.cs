using System;
using System.Collections.Generic;
using System.Linq;
using CamundaClient.Service;
using CamundaClient.Worker;
using CamundaClient.Dto;

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

        public CamundaEngineClient() : this(new Uri(DEFAULT_URL), null, null) { }

        public CamundaEngineClient(Uri restUrl, string userName, string password)
        {
            _camundaClientHelper = new CamundaClientHelper(restUrl, userName, password);
            this.Startup();
        }

        public BpmnWorkflowService BpmnWorkflowService => new BpmnWorkflowService(_camundaClientHelper, _userTasks, _workers);

        public HumanTaskService HumanTaskService => new HumanTaskService(_camundaClientHelper);

        public RepositoryService RepositoryService => new RepositoryService(_camundaClientHelper);

        public ExternalTaskService ExternalTaskService => new ExternalTaskService(_camundaClientHelper);

        public ProcessInstanceService ProcessInstanceService => new ProcessInstanceService(_camundaClientHelper);

        public void Startup()
        {
            //this.ControlTasks();
            this.StartWorkers();
            this.CleanEvents();
            this.RepositoryService.AutoDeploy();
        }

        public void Shutdown()
        {
            this.StopWorkers();
        }

        public void StartWorkers()
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            var externalTaskWorkers = RetrieveExternalTaskWorkerInfo(assembly);

            foreach (var taskWorkerInfo in externalTaskWorkers)
            {
                Console.WriteLine($"Register Task Worker for Topic '{taskWorkerInfo.TopicName}'");
                ExternalTaskWorker worker = new ExternalTaskWorker(ExternalTaskService, taskWorkerInfo);
                _workers.Add(worker);
                worker.StartWork();
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

        // HELPER METHODS

    }
}