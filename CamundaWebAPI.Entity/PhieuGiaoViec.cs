using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Entity
{
    [Table("PhieuGiaoViec")]
    public class PhieuGiaoViec : BaseEntity
    {
        [ExplicitKey]
        public Guid PhieuGiaoViecId { get; set; }
        public Guid NguoiGiaoId { get; set; }
        public string NoiDung { get; set; }
        public string NhanVienThucHien { get; set; }
        public int TrangThai { get; set; }
        public Guid ProcessInstanceId { get; set; }
    }
}
