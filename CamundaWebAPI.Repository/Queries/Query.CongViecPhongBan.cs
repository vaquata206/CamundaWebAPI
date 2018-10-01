using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Repository.Queries
{
    public partial class Query
    {
        public const string GetCongViecPhongBanByChiDaoId = @"SELECT [CongViecPhongBanId]
                          ,[PhongBanId]
                          ,[ChiDaoId]
                          ,[TrangThai]
                          ,[NgayTao]
                          ,[NgaySua]
                          ,[DaXoa]
                      FROM [CongViecPhongBans] WHERE ChiDaoId = @ChiDaoId";

        public const string GetCongViecPhongBanByPhongBanId = @"SELECT [CongViecPhongBanId]
                          ,[PhongBanId]
                          ,[ChiDaoId]
                          ,[TrangThai]
                          ,[NgayTao]
                          ,[NgaySua]
                          ,[DaXoa]
                      FROM [CongViecPhongBans] WHERE PhongBanId = @PhongBanId";
    }
}
