using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.ViewModel
{
    public class ChiDaoViewModel
    {
        public Guid? ChiDaoId { get; set; }
        public Guid CongVanDenId { get; set; }
        public string NoiDung { get; set; }
        public Guid NguoiChiDaoId { get; set; }
        public string PhongBanThucHien { get; set; }
    }
}
