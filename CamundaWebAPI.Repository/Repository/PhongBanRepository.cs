using System.Data;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class PhongBanRepository : RepositoryBase<PhongBan>, IPhongBanRepository
    {
        public PhongBanRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
