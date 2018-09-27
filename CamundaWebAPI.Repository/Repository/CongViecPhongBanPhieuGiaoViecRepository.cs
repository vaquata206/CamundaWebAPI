using System.Data;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class CongViecPhongBanPhieuGiaoViecRepository : RepositoryBase, ICongViecPhongBanPhieuGiaoViecRepository
    {
        public CongViecPhongBanPhieuGiaoViecRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
