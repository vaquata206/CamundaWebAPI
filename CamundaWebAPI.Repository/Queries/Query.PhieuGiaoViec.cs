using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Repository.Queries
{
    public partial class Query
    {
        public const string GetPhieuGiaoViecByCongViecPhongBan = @"SELECT pgv. * 
                FROM [PhieuGiaoViec] as pgv
                JOIN [CongViecPhongBan_PhieuGiaoViec] as cvpb_pgv ON cvpb_pgv.PhieuGiaoViecId = pgv.PhieuGiaoViecId
                WHERE cvpb_pgv.CongViecPhongBanId = @CongViecPhongBanId";
    }
}
