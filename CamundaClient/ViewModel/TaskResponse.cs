using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamundaClient.ViewModel
{
    public class TaskResponse
    {
        public string ProcessInstanceId { get; set; }

        public ResponseInformation Content { get; set; }

        public override string ToString() => ProcessInstanceId;
    }
}
