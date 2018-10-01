using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CamundaWebAPI.Entity;

namespace CamundaWebAPI.Repository.IReposirory
{
    public interface IChiDaoRepository: IRepositoryBase<ChiDao> 
    {
        Task<ChiDao> GetChiDaoByCongVanDenIdAsync(Guid congVanId);
    }
}
