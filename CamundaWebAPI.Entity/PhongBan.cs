using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Entity
{
    public class PhongBan : BaseEntity
    {
        [ExplicitKey]
        public Guid PhongBanId { get; set; }
        public string TenPhongBan { get; set; }
    }
}
