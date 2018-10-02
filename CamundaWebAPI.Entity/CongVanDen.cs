using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Entity
{
    public class CongVanDen : BaseEntity
    {
        [ExplicitKey]
        public Guid CongVanDenId { get; set; }
        public string SoCongVan { get; set; }
        public string TrichYeu { get; set; }
        public int TrangThai { get; set; }
        public Guid ProcessInstanceId { get; set; }
    }
}
