using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CamundaWebAPI.Repository.IReposirory;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Repository.Repository
{
    public abstract class RepositoryBase: IRepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public void Add<T>(T entity) where T : class
        {
            try
            {
                Connection.Insert(entity, transaction: Transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            try
            {
                await Connection.InsertAsync(entity, transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            try
            {
                Connection.Delete(entity, transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            try
            {
                await Connection.DeleteAsync(entity, transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update<T>(T entity) where T : class
        {
            try
            {
                Connection.Update(entity, transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            try
            {
                await Connection.UpdateAsync(entity, transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T Get<T>(object id) where T : class
        {
            try
            {
                return Connection.Get<T>(id, transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<T> GetAsync<T>(object id) where T : class
        {
            try
            {
                return Connection.GetAsync<T>(id, transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            try
            {
                return Connection.GetAll<T>(transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            try
            {
                return Connection.GetAllAsync<T>(transaction: Transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
