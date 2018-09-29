using System.Data;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class PhieuGiaoViecRepository : RepositoryBase<PhieuGiaoViec>, IPhieuGiaoViecRepository
    {
        public PhieuGiaoViecRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
