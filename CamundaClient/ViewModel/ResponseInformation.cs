using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamundaClient.ViewModel
{
    public class ResponseInformation
    {
        public enum Status
        {
            Successed = 200,
            Failed = 400
        }

        public Status StatusResponse { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Variables;
    }
}
