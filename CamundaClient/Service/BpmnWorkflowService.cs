using CamundaClient.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using CamundaClient.Requests;
using System.Text;
using Newtonsoft.Json.Serialization;
using System.Linq;
using CamundaClient.Worker;
using System.Threading;
using System.Threading.Tasks;
using CamundaClient.ViewModel;

namespace CamundaClient.Service
{

    public class BpmnWorkflowService
    {
        private CamundaClientHelper helper;
        private IDictionary<string, HumanTask> _userTasks = new Dictionary<string, HumanTask>();
        private IList<ExternalTaskWorker> _workers = new List<ExternalTaskWorker>();

        public BpmnWorkflowService(CamundaClientHelper client, IDictionary<string, HumanTask> userTasks, IList<ExternalTaskWorker> workers)
        {
            this.helper = client;
            this._userTasks = userTasks;
            this._workers = workers;
        }

        public string StartProcessInstance(string processDefinitionKey, Dictionary<string, object> variables) => StartProcessInstance(processDefinitionKey, null, variables, null);

        public string StartProcessInstance(string processDefinitionKey, Dictionary<string, object> variables, IDictionary<string, Action<IDictionary<string, object>>> events) => StartProcessInstance(processDefinitionKey, null, variables, events);

        public async Task<TaskResponse> StartProcessInstanceAsync(string processDefinitionKey, Dictionary<string, object> variables, string topicName)
        {
            var result = new TaskResponse();

            var request = new CompleteRequest();
            request.Variables = CamundaClientHelper.ConvertVariables(variables);
            var response = await helper.PostAsync(string.Format("process-definition/key/{0}/start", processDefinitionKey), request);
            if (response.IsSuccessStatusCode)
            {
                var processInstance = JsonConvert.DeserializeObject<Dto.ProcessInstance>(response.Content.ReadAsStringAsync().Result);

                if (!string.IsNullOrEmpty(topicName))
                {
                    var externalTask = GetExternalTaskByTopicName(topicName);
                    if (externalTask != null)
                    {
                        var taskCompletionSource = new TaskCompletionSource<object>();
                        if (externalTask.CompletionSources.ContainsKey(processInstance.Id))
                        {
                            var list = externalTask.CompletionSources[processInstance.Id] ?? new List<TaskCompletionSource<object>>();
                            list.Add(taskCompletionSource);
                            externalTask.CompletionSources[processInstance.Id] = list;
                        }
                        else
                        {
                            externalTask.CompletionSources.Add(processInstance.Id, new List<TaskCompletionSource<object>> {
                                taskCompletionSource
                            });
                        }

                        result = (TaskResponse) await taskCompletionSource.Task;
                    }
                }

                return result;
            }
            else
            {
                throw new EngineException(response.ReasonPhrase);
            }
        }

        public string StartProcessInstance(string processDefinitionKey, string businessKey, Dictionary<string, object> variables, IDictionary<string, Action<IDictionary<string, object>>> events)
        {
            var http = helper.HttpClient();

            var request = new CompleteRequest();
            request.Variables = CamundaClientHelper.ConvertVariables(variables);
            request.BusinessKey = businessKey;

            var requestContent = new StringContent(JsonConvert.SerializeObject(request, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), Encoding.UTF8, CamundaClientHelper.CONTENT_TYPE_JSON);
            var response = http.PostAsync("process-definition/key/" + processDefinitionKey + "/start", requestContent).Result;
            if (response.IsSuccessStatusCode)
            {
                var processInstance = JsonConvert.DeserializeObject<Dto.ProcessInstance>(response.Content.ReadAsStringAsync().Result);

                this._userTasks.Add(processInstance.Id, null);
                
                if (events != null)
                {
                    foreach(var evt in events)
                    {
                        var externalTask = GetExternalTaskByTopicName(evt.Key);
                        if (externalTask != null)
                        {
                            if (externalTask.Events.ContainsKey(processInstance.Id))
                            {
                                var list_evt = externalTask.Events[processInstance.Id]?? new List<Action<IDictionary<string, object>>>();
                                list_evt.Add(evt.Value);
                                externalTask.Events[processInstance.Id] = list_evt;
                            }
                            else
                            {
                                externalTask.Events.Add(processInstance.Id, new List<Action<IDictionary<string, object>>>()
                                {
                                    evt.Value
                                });
                            }
                        }
                    }
                }

                return processInstance.Id;
            }
            else
            {
                //var errorMsg = response.Content.ReadAsStringAsync();
                throw new EngineException(response.ReasonPhrase);
            }

        }

        public Dictionary<string, object> LoadVariables(string taskId)
        {
            var http = helper.HttpClient();

            var response = http.GetAsync("task/" + taskId + "/variables").Result;
            if (response.IsSuccessStatusCode)
            {
                // Successful - parse the response body
                var variableResponse = JsonConvert.DeserializeObject< Dictionary<string, Variable>>(response.Content.ReadAsStringAsync().Result);

                var variables = new Dictionary<string, object>();
                foreach (var variable in variableResponse)
                {
                    variables.Add(variable.Key, variable.Value.Value);
                }
                return variables;
            }
            else
            {
                throw new EngineException("Could not load variable: " + response.ReasonPhrase);
            }
        }

        public IList<Dto.ProcessInstance> LoadProcessInstances(IDictionary<string, string> queryParameters)
        {
            var queryString = string.Join("&", queryParameters.Select(x => x.Key + "=" + x.Value));
            var http = helper.HttpClient();

            var response = http.GetAsync("process-instance/?" + queryString).Result;
            if (response.IsSuccessStatusCode)
            {
                // Successful - parse the response body
                var instances = JsonConvert.DeserializeObject<IEnumerable<Dto.ProcessInstance>>(response.Content.ReadAsStringAsync().Result);
                return new List<Dto.ProcessInstance>(instances);
            }
            else
            {
                //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                throw new EngineException("Could not load process instances: " + response.ReasonPhrase);
            }

        }

        private ExternalTaskAdapter GetExternalTaskByTopicName(string topicName)
        {
            return _workers.Where(x => x.taskWorkerInfo.TopicName == topicName).Select(x => x.taskWorkerInfo.TaskAdapter).SingleOrDefault();
        }
    }


}
