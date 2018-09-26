using CamundaClient.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using CamundaClient.Requests;
using Newtonsoft.Json.Serialization;

namespace CamundaClient.Service
{
    public class ProcessInstanceService
    {
        private static string API = "process-instance";
        private CamundaClientHelper helper;

        public ProcessInstanceService(CamundaClientHelper client)
        {
            this.helper = client;
        }

        public IEnumerable<Dto.ProcessInstance> GetInstances()
        {
            var http = helper.HttpClient();

            var response = http.GetAsync(API).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<Dto.ProcessInstance>>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            else
            {
                throw new EngineException("Could not get ProcessInstances: " + response.ReasonPhrase);
            }
        }
    }


}
