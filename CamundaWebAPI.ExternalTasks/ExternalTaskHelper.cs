using System;
using System.Collections.Generic;
using System.Text;
using CamundaClient.Dto;
using Newtonsoft.Json;

namespace CamundaWebAPI.ExternalTasks
{
    public class ExternalTaskHelper
    {
        public static string GetCurrentDomain()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

        public static T GetVariable<T>(Dictionary<string, Variable> variables, string variableName)
        {
            try
            {
                if (!variables.ContainsKey(variableName))
                {
                    return default(T);
                }

                var jsonContent = Convert.ToString(variables[variableName].Value);
                return JsonConvert.DeserializeObject<T>(jsonContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
