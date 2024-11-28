using IHSDC.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IHSDC.Common.Helpers.Database
{
    public static class ApplicationDatabaseHelper
    {
        public static void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new ApplicationDbContext.ApplicationDbInitializer());
            using (var db = new ApplicationDbContext())
            {
                {
                    db.Database.Initialize(true);
                }
            }
        }
    }
}
