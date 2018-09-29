using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using CamundaWebAPI.Repository.IReposirory;
using Dapper.Contrib.Extensions;

namespace CamundaWebAPI.Repository.Repository
{
    public abstract class RepositoryBase<T>: IRepositoryBase<T> where T :class
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public void Add(T entity)
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

        public async Task AddAsync(T entity)
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

        public void Delete(T entity)
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

        public async Task DeleteAsync(T entity)
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

        public void Update(T entity)
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

        public async Task UpdateAsync(T entity)
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

        public T Get(object id)
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

        public Task<T> GetAsync(object id)
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

        public IEnumerable<T> GetAll()
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

        public Task<IEnumerable<T>> GetAllAsync()
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
