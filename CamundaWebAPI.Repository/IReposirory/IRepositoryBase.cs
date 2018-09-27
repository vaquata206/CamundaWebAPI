using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CamundaWebAPI.Repository.IReposirory
{
    public interface IRepositoryBase
    {
        void Add<T>(T entity) where T : class;
        Task AddAsync<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;

        T Get<T>(object id) where T : class;
        Task<T> GetAsync<T>(object id) where T : class;

        IEnumerable<T> GetAll<T>() where T : class;
        Task<IEnumerable<T>> GetAllAsync<T>() where T : class;
    }
}
