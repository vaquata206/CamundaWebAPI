using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Entity
{
    public class CongViecPhongBan : BaseEntity
    {
        public Guid CongViecPhongBanId { get; set; }
        public Guid PhongBanId { get; set; }
        public Guid ChiDaoId { get; set; }
        public int TrangThai { get; set; }
    }
}
