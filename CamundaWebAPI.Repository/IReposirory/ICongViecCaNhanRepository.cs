using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;

namespace CamundaWebAPI.Repository.IReposirory
{
    public interface ICongViecCaNhanRepository: IRepositoryBase<CongViecCaNhan>
    {
        Task<CongViecCaNhan> GetByPhieuGiaoViec(Guid phieuGiaoViecId);
    }
}
