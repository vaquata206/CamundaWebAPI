using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.ViewModel.Request
{
    public class CongVanDenRequest : BaseRequest
    {
        public Guid? CongVanDenId { get; set; }
        public string SoCongVan { get; set; }
        public string TrichYeu { get; set; }
        public int? TrangThai { get; set; }
    }
}
