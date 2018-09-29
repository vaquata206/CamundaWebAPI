using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CamundaWebAPI.Repository.IReposirory
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        Task AddAsync(T entity);

        void Delete(T entity);
        Task DeleteAsync(T entity);

        void Update(T entity);
        Task UpdateAsync(T entity);

        T Get(object id);
        Task<T> GetAsync(object id);

        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
    }
}
