using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Entity
{
    public class PhongBan : BaseEntity
    {
        public Guid PhongBanId { get; set; }
        public string TenPhongBan { get; set; }
    }
}
