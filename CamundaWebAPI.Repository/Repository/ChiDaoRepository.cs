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
    public class ChiDaoRepository : RepositoryBase<ChiDao>, IChiDaoRepository
    {
        public ChiDaoRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public async Task<ChiDao> GetChiDaoByCongVanDenIdAsync(Guid congVanId)
        {
            var chidao = await this.Connection.QueryFirstOrDefaultAsync<ChiDao>(
                        Query.GetChiDaoByCongVanDenIdAsync,
                        param: new
                        {
                            CongVanDenId = congVanId
                        },
                        transaction: this.Transaction,
                        commandTimeout: Constants.CommandTimeout);

            return chidao;
        }
    }
}
