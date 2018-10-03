using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Response
{
    public class CongViecPhongBanResponse
    {
        public Guid CongViecPhongBanId { get; set; }
        public Guid PhongBanId { get; set; }
        public Guid ChiDaoId { get; set; }
        public int TrangThai { get; set; }
        public string NoiDungChiDao { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgaySua { get; set; }
        public bool DaXoa { get; set; }
        public Guid ProcessInstanceId { get; set; }
    }
}
