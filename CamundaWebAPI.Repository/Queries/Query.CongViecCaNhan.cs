using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Repository.Queries
{
    public partial class Query
    {
        public const string GetCVCNByPhieuGiaoViec = @"SELECT [PhieuGiaoViecId]
                                                  ,[NguoiGiaoId]
                                                  ,[NoiDung]
                                                  ,[NhanVienThucHien]
                                                  ,[TrangThai]
                                                  ,[NgayTao]
                                                  ,[NgaySua]
                                                  ,[DaXoa]
                                              FROM [PhieuGiaoViec] WHERE PhieuGiaoViecId =  @PhieuGiaoViecId";
    }
}
