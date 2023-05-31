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

    }
}
