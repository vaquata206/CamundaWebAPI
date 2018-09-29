using System.Data;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class CongViecPhongBanRepository : RepositoryBase<CongViecPhongBan>, ICongViecPhongBanRepository
    {
        public CongViecPhongBanRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
