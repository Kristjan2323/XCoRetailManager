using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRMDataManager.Library.Internal.DatatAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private string connStringName = "DefaultConnection";
        // read data from database
        public List<T> LoadData<T, U>(string storeProcedure, U parameters)
        {
            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
            {
                var rows = conn.Query<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
                return rows.ToList();
            }
        }

        //write data in database
        public void SaveData<T>(string storeProcedure, T parameters)
        {

            using (IDbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString))
            {
                conn.Execute(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public void StartTransaction()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connStringName].ConnectionString);
            _connection?.Open();
            _transaction = _connection?.BeginTransaction();
        }

        public void SaveDataInTransiction<T>(string storeProcedure, T parameters)
        {
            StartTransaction();
            _connection.Execute(storeProcedure, parameters,
              commandType: CommandType.StoredProcedure, transaction: _transaction);
          
        }

        public List<T> LoadDataInTransaction<T, U>(string storeProcedure, U parameters)
        {
            StartTransaction();
            return _connection.Query<T>(storeProcedure, parameters,
                  commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
          
        }
        public void CommitTransaction()
        {
            _transaction?.Commit();
            _connection?.Close();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _connection?.Close();
        }

        public void Dispose()
        {
            CommitTransaction();
        }
    }
}
