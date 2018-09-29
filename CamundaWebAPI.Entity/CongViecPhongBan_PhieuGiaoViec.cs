using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Entity
{
    [Table("CongViecPhongBan_PhieuGiaoViec")]
    public class CongViecPhongBan_PhieuGiaoViec
    {
        public Guid Id { get; set; }
        public Guid CongViecPhongBanId { get; set; }
        public Guid PhieuGiaoViecId { get; set; }
        public int? Loai { get; set; }
        public DateTime NgayTao { get; set; }
        public bool DaXoa { get; set; }
    }
}
