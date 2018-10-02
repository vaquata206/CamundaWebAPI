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
    public class CongViecCaNhanRepository : RepositoryBase<CongViecCaNhan>, ICongViecCaNhanRepository
    {
        public CongViecCaNhanRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<CongViecCaNhan> GetByPhieuGiaoViec(Guid phieuGiaoViecId)
        {
            return await this.Connection.QueryFirstOrDefaultAsync<CongViecCaNhan>(
                Query.GetCVCNByPhieuGiaoViec,
                param: new { PhieuGiaoViecId = phieuGiaoViecId },
                commandTimeout: Constants.CommandTimeout);
        }

        public async Task<IEnumerable<CongViecCaNhanResponse>> GetDsCongViecCaNhanByCaNhanIdAsync(Guid caNhanId)
        {
            return await this.Connection.QueryAsync<CongViecCaNhanResponse>(
                Query.GetDsCongViecCaNhanByCaNhanId,
                param: new { CaNhanId = caNhanId },
                commandTimeout: Constants.CommandTimeout);
        }
    }
}
