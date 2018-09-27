using System.Data;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class PhieuGiaoViecRepository : RepositoryBase, IPhieuGiaoViecRepository
    {
        public PhieuGiaoViecRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
