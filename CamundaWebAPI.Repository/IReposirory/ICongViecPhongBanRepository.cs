using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;
using CamundaWebAPI.ViewModel.Response;

namespace CamundaWebAPI.Repository.IReposirory
{
    public interface ICongViecPhongBanRepository: IRepositoryBase<CongViecPhongBan>
    {
        Task<IEnumerable<CongViecPhongBan>> GetCongViecPhongBanByChiDaoIdAsync(Guid chiDaoId);
        Task<IEnumerable<CongViecPhongBanResponse>> GetDsCongViecPhongBanByPhongBanIdAsync(Guid phongBanId);
        Task<CongViecPhongBanResponse> GetCongViecPhongBanByIdAsync(Guid id);
    }
}
