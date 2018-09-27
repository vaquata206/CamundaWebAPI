using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Request
{
    public class BaseRequest
    {
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public bool? DaXoa { get; set; }
    }
}
