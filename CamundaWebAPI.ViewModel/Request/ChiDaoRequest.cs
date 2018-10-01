using CamundaWebAPI.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Request
{
    public class ChiDaoRequest : BaseRequest
    {
        public Guid ProcessInstanceId { get; set; }
        public Guid TaskId { get; set; }
        public ChiDaoViewModel ChiDao { get; set; }
    }
}
