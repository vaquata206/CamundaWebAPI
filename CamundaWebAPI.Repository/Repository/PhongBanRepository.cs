using System.Data;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class PhongBanRepository : RepositoryBase, IPhongBanRepository
    {
        public PhongBanRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
