using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.Common;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Queries;
using CamundaWebAPI.Repository.Repository;
using Dapper;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class CongVanDenRepository : RepositoryBase<CongVanDen>, ICongVanDenRepository
    {
        public CongVanDenRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<IEnumerable<CongVanDen>> GetDsCongVanDenAsync()
        {
            var list = await this.Connection.QueryAsync<CongVanDen>(
                Query.GetDsCongVanDen,
                transaction: this.Transaction,
                commandTimeout: Constants.CommandTimeout);

            return list;
        }
    }
}
