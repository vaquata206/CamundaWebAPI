using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;
using CamundaWebAPI.ViewModel.Response;

namespace CamundaWebAPI.Repository.IReposirory
{
    public interface ICongViecCaNhanRepository: IRepositoryBase<CongViecCaNhan>
    {
        Task<CongViecCaNhan> GetByPhieuGiaoViec(Guid phieuGiaoViecId);
        Task<IEnumerable<CongViecCaNhanResponse>> GetDsCongViecCaNhanByCaNhanIdAsync(Guid caNhanId);
    }
}
