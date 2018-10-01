using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Entity
{
    [Table("ChiDao")]
    public class ChiDao: BaseEntity
    {
        [ExplicitKey]
        public Guid ChiDaoId { get; set; }
        public Guid CongVanDenId { get; set; }
        public string NoiDung { get; set; }
        public Guid NguoiChiDaoId { get; set; }
        public string PhongBanThucHien { get; set; }
    }
}
