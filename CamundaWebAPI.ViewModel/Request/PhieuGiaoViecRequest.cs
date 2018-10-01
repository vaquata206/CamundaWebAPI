using CamundaWebAPI.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Request
{
    public class PhieuGiaoViecRequest : BaseRequest
    {
        public Guid ProcessInstanceId { get; set; }
        public Guid TaskId { get; set; }
        public PhieuGiaoViecViewModel PhieuGiaoViec { get; set; }
    }
}
