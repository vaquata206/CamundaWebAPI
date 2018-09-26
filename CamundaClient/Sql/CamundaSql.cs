using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamundaClient.Sql
{
    public class CamundaSql : IDisposable
    {
        private SqlTransaction _transaction;
        private SqlConnection _connection;
        private string _processInstanceId;

        public CamundaSql(string connectionString, string processInstanceId) : this(connectionString, processInstanceId, false) { }

        public CamundaSql(string connectionString, string processInstanceId, bool isTransaction)
        {
            try
            {
                this._processInstanceId = processInstanceId;
                _connection = new SqlConnection(connectionString);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //public CamundaTransaction GetTransaction()
        //{
        //    return _transaction ?? new CamundaTransaction(_connection);
        //}

        public void ExecuteNonQuery(string sqlString)
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }

            if (_transaction == null)
            {
                _transaction = _connection.BeginTransaction();
            }

            try
            {
                SqlCommand command = _connection.CreateCommand();
                command.Transaction = _transaction;
                command.CommandText = sqlString;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                this.Dispose();
                throw ex;
            }
        }

        public void Submit()
        {
            this._transaction.Commit();
            this.Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                this._transaction.Dispose();
            }

            _connection.Dispose();
            CamundaSqlManager.RemoveCamundaSql(this._processInstanceId, _connection.ConnectionString);
        }
    }
}
