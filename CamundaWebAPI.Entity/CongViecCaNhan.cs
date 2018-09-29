using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Entity
{
    public class CongViecCaNhan : BaseEntity
    {
        public Guid CongViecCaNhanId { get; set; }
        public Guid CaNhanId { get; set; }
        public Guid PhieuGiaoViecId { get; set; }
        public string NoiDungThucHien { get; set; }
        public int TrangThai { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
    }
}
