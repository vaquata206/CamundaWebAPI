using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Entity
{
    public class PhieuGiaoViec : BaseEntity
    {
        public Guid PhieuGiaoViecId { get; set; }
        public Guid NguoiGiaoId { get; set; }
        public string NoiDung { get; set; }
        public string NhanVienThucHien { get; set; }
        public int TrangThai { get; set; }
    }
}
