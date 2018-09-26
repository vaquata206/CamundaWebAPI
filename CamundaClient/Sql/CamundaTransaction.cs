using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamundaClient.Sql
{
    public class CamundaTransaction: IDisposable
    {
        private SqlTransaction _sqlTransaction;
        
        public CamundaTransaction(SqlConnection sqlConnection)
        {
            _sqlTransaction = sqlConnection.BeginTransaction();
        }

        public void SaveChange()
        {

        }

        public void Dispose()
        {
            this._sqlTransaction.Dispose();
        }
    }
}
