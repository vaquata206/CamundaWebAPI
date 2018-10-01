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
    public class CongViecPhongBanRepository : RepositoryBase<CongViecPhongBan>, ICongViecPhongBanRepository
    {
        public CongViecPhongBanRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<IEnumerable<CongViecPhongBan>> GetCongViecPhongBanByChiDaoIdAsync(Guid chiDaoId)
        {
            var list = await this.Connection.QueryAsync<CongViecPhongBan>(
                Query.GetCongViecPhongBanByChiDaoId, 
                param: new { ChiDaoId = chiDaoId }, 
                transaction: this.Transaction, 
                commandTimeout:Constants.CommandTimeout);

            return list;
        }

        public async Task<IEnumerable<CongViecPhongBan>> GetCongViecPhongBanByPhongBanIdAsync(Guid phongBanId)
        {
            var list = await this.Connection.QueryAsync<CongViecPhongBan>(
                Query.GetCongViecPhongBanByPhongBanId,
                param: new { PhongBanId = phongBanId },
                transaction: this.Transaction,
                commandTimeout: Constants.CommandTimeout);

            return list;
        }
    }
}
