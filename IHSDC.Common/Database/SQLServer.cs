using System;
using System.Data.SqlClient;

namespace IHSDC.Common.Database
{
    public class SQLServer
    {
        public static string GetConnectionString()
        {
            string connectionString =
                Environment.GetEnvironmentVariable(
                    "ACMS_DB_CONNECTION",
                    EnvironmentVariableTarget.Machine);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Environment variable ACMS_DB_CONNECTION is missing.");
            }

            return connectionString;
        }
    }
}