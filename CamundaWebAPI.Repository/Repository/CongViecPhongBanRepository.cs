using System.Data;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class CongViecPhongBanRepository : RepositoryBase, ICongViecPhongBanRepository
    {
        public CongViecPhongBanRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
