using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Entity
{
    public class NhanVien
    {
        [ExplicitKey]
        public Guid NhanVienId { get; set; }
        public string TenNhanVien { get; set; }
    }
}
