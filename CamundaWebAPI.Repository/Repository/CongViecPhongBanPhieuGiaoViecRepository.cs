using System.Data;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class CongViecPhongBanPhieuGiaoViecRepository : RepositoryBase<CongViecPhongBan_PhieuGiaoViec>, ICongViecPhongBanPhieuGiaoViecRepository
    {
        public CongViecPhongBanPhieuGiaoViecRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
