using System;
using System.Collections.Generic;
using System.Text;

namespace CamundaWebAPI.Repository.IReposirory
{
    public interface IUnitOfWork : IDisposable
    {
        IChiDaoRepository ChiDaoRepository { get; }
        ICongVanDenRepository CongVanDenRepository { get; }
        ICongViecPhongBanRepository CongViecPhongBanRepository { get; }
        ICongViecPhongBanPhieuGiaoViecRepository CongViecPhongBanPhieuGiaoViecRepository { get; }
        INhanVienRepository NhanVienRepository { get; }
        IPhieuGiaoViecRepository PhieuGiaoViecRepository { get; }
        IPhongBanRepository PhongBanRepository { get; }
        ICongViecCaNhanRepository CongViecCaNhanRepository { get; }

        void Commit();
    }
}
