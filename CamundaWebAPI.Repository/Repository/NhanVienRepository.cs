using System.Data;
using CamundaWebAPI.Entity;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class NhanVienRepository : RepositoryBase<NhanVien>, INhanVienRepository
    {
        public NhanVienRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
