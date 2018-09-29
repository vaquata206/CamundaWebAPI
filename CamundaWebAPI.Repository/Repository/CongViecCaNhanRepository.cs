using System.Data;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class CongViecCaNhanRepository : RepositoryBase<CongViecCaNhan>, ICongViecCaNhanRepository
    {
        public CongViecCaNhanRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
