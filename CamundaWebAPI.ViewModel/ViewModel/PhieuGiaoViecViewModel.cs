using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.ViewModel
{
    public class PhieuGiaoViecViewModel
    {
        public Guid? PhieuGiaoViecId { get; set; }
        public Guid NguoiGiaoId { get; set; }
        public string NoiDung { get; set; }
        public string NhanVienThucHien { get; set; }
        public int? TrangThai { get; set; }
    }
}
