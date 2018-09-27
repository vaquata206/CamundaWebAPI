using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Entity
{
    public class BaseEntity
    {
        public DateTime NgayTao { get; set; }

        public DateTime NgaySua { get; set; }

        public bool DaXoa { get; set; }
    }
}
