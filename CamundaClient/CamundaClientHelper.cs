using CamundaClient.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CamundaClient
{

    public class CamundaClientHelper
    {
        public Uri RestUrl { get; }
        public const string CONTENT_TYPE_JSON = "application/json";
        public string RestUsername { get; }
        public string RestPassword { get; }

        private static HttpClient client;

        public CamundaClientHelper(Uri restUrl, string username, string password)
        {
            this.RestUrl = restUrl;
            this.RestUsername = username;
            this.RestPassword = password;
        }

        public HttpClient HttpClient()
        {
            try
            {
                if (client == null)
                {
                    if (RestUsername != null)
                    {
                        var credentials = new NetworkCredential(RestUsername, RestPassword);
                        client = new HttpClient(new HttpClientHandler() { Credentials = credentials });
                    }
                    else
                    {
                        client = new HttpClient();
                    }
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(CONTENT_TYPE_JSON));
                    client.BaseAddress = RestUrl;
                }
            }
            catch (Exception) { }
            return client;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string requestURL, T request)
        {
            try
            {
                var http = this.HttpClient();
                var requestContent = new StringContent(JsonConvert.SerializeObject(request, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }), Encoding.UTF8, CamundaClientHelper.CONTENT_TYPE_JSON);
                var response = await http.PostAsync(requestURL, requestContent);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Dictionary<string, Variable> ConvertVariables(Dictionary<string, object> variables)
        {
            // report successful execution
            var result = new Dictionary<string, Variable>();
            if (variables == null)
            {
                return result;
            }
            foreach (var variable in variables)
            {
                Variable camundaVariable = new Variable
                {
                    Value = variable.Value
                };
                result.Add(variable.Key, camundaVariable);
            }
            return result;
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["database"].ConnectionString;
        }
    }
}
