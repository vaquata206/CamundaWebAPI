using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CamundaWebAPI.Entity
{
    [Table("ChiDao")]
    public class ChiDao: BaseEntity
    {
        public Guid ChiDaoId { get; set; }
        public Guid CongVanDenId { get; set; }
        public string NoiDung { get; set; }
        public Guid NguoiChiDaoId { get; set; }
        public string PhongBanThucHien { get; set; }
    }
}
