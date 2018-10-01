using CamundaWebAPI.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Request
{
    public class PhieuGiaoViecRequest
    {
        public Guid ProcessInstanceId { get; set; }
        public Guid TaskId { get; set; }
        public Guid CongViecPhongBanId { get; set; }
        public PhieuGiaoViecViewModel PhieuGiaoViec { get; set; }
    }
}
