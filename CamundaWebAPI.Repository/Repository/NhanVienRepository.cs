using System.Data;
using CamundaWebAPI.Repository.IReposirory;
using CamundaWebAPI.Repository.Repository;

namespace CamundaWebAPI.Repository.Reposirory
{
    public class NhanVienRepository : RepositoryBase, INhanVienRepository
    {
        public NhanVienRepository(IDbTransaction transaction) : base(transaction)
        {
        }
    }
}
