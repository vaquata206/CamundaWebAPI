using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Repository.Queries
{
    public partial class Query
    {
        public const string GetCVCNByPhieuGiaoViec = @"SELECT [CongViecCaNhanId]
                                                          ,[CaNhanId]
                                                          ,[PhieuGiaoViecId]
                                                          ,[NoiDungThucHien]
                                                          ,[TrangThai]
                                                          ,[NgayHoanThanh]
                                                          ,[NgayTao]
                                                          ,[NgaySua]
                                                          ,[DaXoa]
                                                      FROM [CongViecCaNhan] WHERE PhieuGiaoViecId =  @PhieuGiaoViecId";
    }
}
