using IHSDC.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHSDC.Common.Database
{
    public class SQLServer
    {
        public static string GetConnectionString()
        {
            string serverName = EF.DatabaseServer;
            string databaseName = EF.DatabaseName;
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                InitialCatalog = databaseName,
                IntegratedSecurity = false,
                ConnectTimeout = 30,
                Encrypt = false,
                TrustServerCertificate = false,
                ApplicationIntent = ApplicationIntent.ReadWrite,
                MultiSubnetFailover = false
            };
            sqlBuilder.ConnectTimeout = 15;
            sqlBuilder.UserID = EF.DatabaseUsername;
            sqlBuilder.Password = EF.DatabasePassword;

            string providerString = sqlBuilder.ToString();

            return sqlBuilder.ToString();
        }
    }
}
