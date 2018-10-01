using System;
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
    }
}
