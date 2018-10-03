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

        public const string GetDsCongViecPhongBanByPhongBanId = @"SELECT cvpb.CongViecPhongBanId as CongViecPhongBanId
	                       ,cvpb.PhongBanId as PhongBanId
	                       ,cvpb.ChiDaoId as ChiDaoId
                           ,cvpb.TrangThai as TrangThai
	                       ,cd.NoiDung as NoiDungChiDao
	                       ,cd.NgayTao as NgayTao
	                       ,cd.NgaySua as NgaySua
	                       ,cd.DaXoa as DaXoa
                      FROM [CongViecPhongBans] as cvpb
                      left join [ChiDao] as cd on cvpb.ChiDaoId = cd.ChiDaoId
                      WHERE cvpb.PhongBanId = @PhongBanId
                      ORDER BY cd.NgayTao DESC";

        public const string GetCongViecPhongBanById = @"SELECT cvpb.CongViecPhongBanId as CongViecPhongBanId
	                       ,cvpb.PhongBanId as PhongBanId
	                       ,cvpb.ChiDaoId as ChiDaoId
                           ,cvpb.TrangThai as TrangThai
	                       ,cd.NoiDung as NoiDungChiDao
	                       ,cd.NgayTao as NgayTao
	                       ,cd.NgaySua as NgaySua
	                       ,cd.DaXoa as DaXoa
						   ,cvd.ProcessInstanceId as ProcessInstanceId
                      FROM [CongViecPhongBans] as cvpb
                      inner join [ChiDao] as cd on cvpb.ChiDaoId = cd.ChiDaoId
                      inner join [CongVanDens] as cvd on cvd.CongVanDenId = cd.CongVanDenId
                      WHERE cvpb.CongViecPhongBanId = @CongViecPhongBanId";
    }
}
