using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Entity
{
    public class CongVanDen : BaseEntity
    {
        public Guid CongVanDenId { get; set; }
        public string SoCongVan { get; set; }
        public string TrichYeu { get; set; }
        public int TrangThai { get; set; }
    }
}
