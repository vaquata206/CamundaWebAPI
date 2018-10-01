using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Entity
{
    public class CongViecPhongBan : BaseEntity
    {
        [ExplicitKey]
        public Guid CongViecPhongBanId { get; set; }
        public Guid PhongBanId { get; set; }
        public Guid ChiDaoId { get; set; }
        public int TrangThai { get; set; }
    }
}
