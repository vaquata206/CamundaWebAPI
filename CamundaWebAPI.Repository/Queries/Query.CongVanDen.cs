using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Repository.Queries
{
    public partial class Query
    {
        public const string GetDsCongVanDen = @"SELECT [CongVanDenId]
                        ,[SoCongVan]
                        ,[TrichYeu]
                        ,[TrangThai]
                        ,[NgayTao]
                        ,[NgaySua]
                        ,[DaXoa]
                        ,[ProcessId]
                        FROM [CongVanDens] WHERE DaXoa = 0 ORDER BY NgayTao DESC";
    }
}
