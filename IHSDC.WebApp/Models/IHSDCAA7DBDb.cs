using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AA7.Models
{
    public class IHSDCAA7DBDBContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    
           
        
       
           
            modelBuilder.Entity<dbo_FullHierarchy>().ToTable("dbo.FullHierarchy");
      
         
            modelBuilder.Entity<dbo_tbl_Unit>().ToTable("dbo.tbl_Unit");
            
        }
        
 

        public DbSet<dbo_FullHierarchy> dbo_FullHierarchy { get; set; }


        public DbSet<dbo_tbl_Unit> dbo_tbl_Unit { get; set; }
    

    }
}
 
