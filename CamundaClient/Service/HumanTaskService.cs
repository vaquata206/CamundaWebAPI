﻿using CamundaClient.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using CamundaClient.Requests;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace CamundaClient.Service
{

    public class HumanTaskService
    {
        private CamundaClientHelper helper;

        public HumanTaskService(CamundaClientHelper client)
        {
            this.helper = client;
        }

        public IList<HumanTask> LoadTasks() => LoadTasks(new Dictionary<string, string>());

        public IList<HumanTask> LoadTasks(IDictionary<string, string> queryParameters)
        {
            var queryString = string.Join("&", queryParameters.Select(x => x.Key + "=" + x.Value));
            var http = helper.HttpClient();

            var response = http.GetAsync("task/?" + queryString).Result;
            if (response.IsSuccessStatusCode)
            {
                // Successful - parse the response body
                var tasks = JsonConvert.DeserializeObject<IEnumerable<HumanTask>>(response.Content.ReadAsStringAsync().Result);
                return new List<HumanTask>(tasks);
            }
            else
            {
                //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                throw new EngineException("Could not load tasks: " + response.ReasonPhrase);
            }

        }

        public Dictionary<string, object> LoadVariables(string taskId)
        {
            var http = helper.HttpClient();

            var response = http.GetAsync("task/" + taskId + "/variables").Result;
            if (response.IsSuccessStatusCode)
            {
                // Successful - parse the response body
                var variableResponse = JsonConvert.DeserializeObject<Dictionary<string, Variable>>(response.Content.ReadAsStringAsync().Result);

                Dictionary<string, object> variables = new Dictionary<string, object>();
                foreach (var variable in variableResponse)
                {
                    if (variable.Value.Type == "object")
                    {
                        string stringValue = (string)variable.Value.Value;
                        // lets assume we only work with JSON serialized values 
                        stringValue = stringValue.Remove(stringValue.Length - 1).Remove(0, 1); // remove one bracket from {{ and }}
                        var jsonObject = JContainer.Parse(stringValue);

                        variables.Add(variable.Key, jsonObject);
                    }
                    else
                    {
                        variables.Add(variable.Key, variable.Value.Value);
                    }
                }
                return variables;
            }
            else
            {
                throw new EngineException("Could not fetch and lock tasks: " + response.ReasonPhrase);
            }
        }

        public void Complete(string taskId, Dictionary<string, object> variables)
        {
            var http = helper.HttpClient();

            var request = new CompleteRequest();
            request.Variables = CamundaClientHelper.ConvertVariables(variables);

            var requestContent = new StringContent(JsonConvert.SerializeObject(request, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), Encoding.UTF8, CamundaClientHelper.CONTENT_TYPE_JSON);
            var response = http.PostAsync("task/" + taskId + "/complete", requestContent).Result;
            if (!response.IsSuccessStatusCode)
            {
                //var errorMsg = response.Content.ReadAsStringAsync();
                throw new EngineException(response.ReasonPhrase);
            }
        }

        public async Task<List<HumanTask>> LoadTask(string processInstanceId)
        {
            var http = helper.HttpClient();

            var response = await http.GetAsync("task?processInstanceId=" + processInstanceId);

            if (response.IsSuccessStatusCode)
            {
                var taskResponse = JsonConvert.DeserializeObject<List<HumanTask>>(response.Content.ReadAsStringAsync().Result);

                return taskResponse;
            }
            else
            {
                throw new EngineException("Could not load variable: " + response.ReasonPhrase);
            }
        }

        public async Task<HumanTask> LoadTask(string processInstanceId, string taskName)
        {
            var http = helper.HttpClient();

            var response = await http.GetAsync("task?processInstanceId=" + processInstanceId);

            if (response.IsSuccessStatusCode)
            {
                var taskResponse = JsonConvert.DeserializeObject<List<HumanTask>>(response.Content.ReadAsStringAsync().Result);

                return taskResponse.Where(p => p.Name.Equals(taskName)).SingleOrDefault();
            }
            else
            {
                throw new EngineException("Could not load variable: " + response.ReasonPhrase);
            }
        }

        public async Task CompleteTask(string taskId, Dictionary<string, object> variables)
        {
            var request = new CompleteRequest();
            request.Variables = CamundaClientHelper.ConvertVariables(variables);

            var http = helper.HttpClient();
            var requestContent = new StringContent(JsonConvert.SerializeObject(variables, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), Encoding.UTF8, CamundaClientHelper.CONTENT_TYPE_JSON);
            var response = await http.PostAsync("task/" + taskId + "/complete", requestContent);

            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new EngineException("Could not load variable: " + response.ReasonPhrase);
            }
        }
    }


}
