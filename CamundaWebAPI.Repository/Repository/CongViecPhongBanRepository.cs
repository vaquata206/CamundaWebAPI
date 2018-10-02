using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.Common;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Queries;
using CamundaWebAPI.Repository.Repository;
using CamundaWebAPI.ViewModel.Response;
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

        public async Task<IEnumerable<CongViecPhongBanResponse>> GetDsCongViecPhongBanByPhongBanIdAsync(Guid phongBanId)
        {
            var list = await this.Connection.QueryAsync<CongViecPhongBanResponse>(
                Query.GetDsCongViecPhongBanByPhongBanId,
                param: new { PhongBanId = phongBanId },
                transaction: this.Transaction,
                commandTimeout: Constants.CommandTimeout);

            return list;
        }

        public async Task<CongViecPhongBanResponse> GetCongViecPhongBanByIdAsync(Guid id)
        {
            var result = await this.Connection.QueryFirstAsync<CongViecPhongBanResponse>(
                   Query.GetCongViecPhongBanById,
                   param: new { CongViecPhongBanId = id },
                   transaction: this.Transaction,
                   commandTimeout: Constants.CommandTimeout);
            return result;
        }
    }
}
