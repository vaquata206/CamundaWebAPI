using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.Common;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Queries;
using CamundaWebAPI.Repository.Repository;
using Dapper;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class PhieuGiaoViecRepository : RepositoryBase<PhieuGiaoViec>, IPhieuGiaoViecRepository
    {
        public PhieuGiaoViecRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<PhieuGiaoViec> GetByCongViecPhongBan(Guid congViecPhongBanId)
        {
            return await this.Connection.QueryFirstOrDefaultAsync<PhieuGiaoViec>(
                Query.GetPhieuGiaoViecByCongViecPhongBan,
                param: new
                {
                    CongViecPhongBanId = congViecPhongBanId
                },
                transaction: this.Transaction,
                commandTimeout: Constants.CommandTimeout
                );
        }
    }
}
