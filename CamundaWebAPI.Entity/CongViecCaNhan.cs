using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Entity
{
    [Table("CongViecCaNhan")]
    public class CongViecCaNhan : BaseEntity
    {
        [ExplicitKey]
        public Guid CongViecCaNhanId { get; set; }
        public Guid CaNhanId { get; set; }
        public Guid PhieuGiaoViecId { get; set; }
        public string NoiDungThucHien { get; set; }
        public int TrangThai { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
    }
}
