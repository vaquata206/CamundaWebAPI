using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamundaClient.Sql
{
    public class CamundaSqlManager
    {
        private static IDictionary<string, CamundaSql> _camundaSqls = new Dictionary<string, CamundaSql>();

        public static CamundaSql GetCamundaSql(string processInstanceId ,string connectionString)
        {
            var key = GenerateKey(processInstanceId, connectionString);
            if (!CamundaSqlManager._camundaSqls.ContainsKey(key))
            {
                CamundaSqlManager._camundaSqls.Add(key, new CamundaSql(connectionString, processInstanceId));
            }

            return CamundaSqlManager._camundaSqls[key];
        }

        public static void RemoveCamundaSql(string processInstanceId, string connectionString)
        {
            var key = GenerateKey(processInstanceId, connectionString);
            if (CamundaSqlManager._camundaSqls.ContainsKey(key))
            {
                CamundaSqlManager._camundaSqls.Remove(key);
            }
        }

        private static string GenerateKey(string processInstanceId, string connectionString)
        {
            return string.Format("{0}_{1}", processInstanceId, connectionString);
        }
    }
}
