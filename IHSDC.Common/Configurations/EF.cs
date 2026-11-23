//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace IHSDC.Common.Configurations
//{
//    using IHSDC.Common.Database;

//    /// <summary>
//    /// This class defines Database constants
//    /// </summary>
//    /// <remarks>
//    /// The properties are self explanatory and can be changed as per requirement.
//    /// </remarks>
//    using System;
//    using System.Data.SqlClient;

//    public class EF
//    {
//        private static SqlConnectionStringBuilder GetBuilder()
//        {
//            return new SqlConnectionStringBuilder(
//                SQLServer.GetConnectionString());
//        }

//        public static string DatabaseName =>
//            GetBuilder().InitialCatalog;

//        public const string DatabaseSchema = Application.Abbreviation;

//        public static string DatabaseServer =>
//            GetBuilder().DataSource;

//        public static string DatabaseUsername =>
//            GetBuilder().UserID;

//        public static string DatabasePassword =>
//            GetBuilder().Password;
//    }
//}
