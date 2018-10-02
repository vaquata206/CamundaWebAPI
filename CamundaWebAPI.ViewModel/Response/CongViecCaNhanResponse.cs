using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Response
{
    public class CongViecCaNhanResponse
    {
        public Guid CongViecCaNhanId { get;set; }
        public Guid CaNhanId { get; set; }
        public Guid PhieuGiaoViecId { get; set; }
        public string NoiDung { get; set; }
        public int TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public bool DaXoa { get; set; }
        public Guid ProcessInstanceId { get; set; }
    }
}
