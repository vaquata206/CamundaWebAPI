using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Repository.Queries
{
    public partial class Query
    {
        public const string GetChiDaoByCongVanDenIdAsync = @"SELECT [ChiDaoId]
                          ,[CongVanDenId]
                          ,[NoiDung]
                          ,[NguoiChiDaoId]
                          ,[PhongBanThucHien]
                          ,[NgayTao]
                          ,[NgaySua]
                          ,[DaXoa]
                        FROM [ChiDao] WHERE CongVanDenId = @CongVanDenId";
    }
}
