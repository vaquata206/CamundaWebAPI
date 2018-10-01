using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Request
{
    public class TrangThaiRequest
    {
        public Guid TaskId { get; set; }
        public Guid ProcessInstanceId { get; set; }
        public int TrangThai { get; set; }
    }
}
