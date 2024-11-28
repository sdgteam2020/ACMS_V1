using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace IHSDC.WebApp.Helper
{
    public class ErrorLog
    {
        public static StringBuilder BindErrorLog(SqlException ex)
        {
            StringBuilder errorMessages = new StringBuilder();
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                errorMessages.Append("Index #" + i + "\n" +
                    "Message: " + ex.Errors[i].Message + "\n" +
                    "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                    "Source: " + ex.Errors[i].Source + "\n" +
                    "Procedure: " + ex.Errors[i].Procedure + "\n");
            }
            return errorMessages;
        }
    }
}