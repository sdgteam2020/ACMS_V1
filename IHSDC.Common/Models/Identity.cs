using IHSDC.Common.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IHSDC.Common.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            // IntId
            // Unit/ Fmn


            return userIdentity;
        }



        /// <summary>
        /// All additional properties for user comes here
        /// </summary>
        #region Application User Properties
        public int IntId { get; set; }
        public string DEFwdAuth { get; set; }
        public string GEBFwdAuth { get; set; }
        public string Active { get; set; }
        public string EstablishmentFull { get; set; }
        //public string Sqn { get; set; }
        public string EstablishmentAbbreviation { get; set; }
        public string Appointment { get; set; }
        public string PersonnelNumber { get; set; }
        public string Rank { get; set; }
        public string FullName { get; set; }
        public bool IsPasswordHealthy { get; set; }
        public int Unit_ID { get; set; }
        public string MobNo { get; set; }

        public int UserTypeId { get; set; }
        public int ParentId { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        #endregion

        /// <summary>
        /// Donot change anything below
        /// </summary>
        #region Audit Properties
        [Display(Name = "Created By")]
        public int CreatedByIntId { get; set; }

        [Display(Name = "Created By")]
        public int CreatedByGuid { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        #endregion

        ///<summary>
        ///Hierarchy Logic Below
        ///DONOT EXPERIMENT BELOW
        ///</summary>
        [Display(Name = "Superior Id")]
        public string SuperiorId { get; set; }
        public ApplicationUser Superior { get; set; }
        public virtual ICollection<ApplicationUser> Subordinates { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(SQLServer.GetConnectionString(), throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            System.Data.Entity.Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        #region DbSets
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Hierarchy> Hierarchy { get; set; }
        public DbSet<FullHierarchy> FullHierarchy { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Login> Logins { get; set; }

        public DbSet<UserHandingTakingOver> HandingTakings { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("ModelBuilder is Null or Empty");
            }

            base.OnModelCreating(modelBuilder);     //Never ever remove this line

            #region ApplicationUser Mapping
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<ApplicationUser>()
                .Property(k => k.IntId)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Subordinates)
                .WithOptional(p => p.Superior)
                .HasForeignKey(k => k.SuperiorId);
            modelBuilder.Entity<ApplicationRole>().HasKey<string>(r => r.Id).ToTable("AspNetRoles");
            modelBuilder.Entity<ApplicationUser>().HasMany<ApplicationUserRole>((ApplicationUser u) => u.UserRoles);
            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");
            #endregion

            #region Hierarchy Mapping
            modelBuilder.Entity<Hierarchy>().ToTable("AspNetHierarchy");
            modelBuilder.Entity<Hierarchy>()
                .HasKey(k => k.Id)
                .Property(k => k.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<FullHierarchy>().ToTable("AspNetFullHierarchy");
            modelBuilder.Entity<FullHierarchy>()
                .HasKey(k => k.Id)
                .Property(k => k.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UserHandingTakingOver>().ToTable("AspNetUserHandingTakingOver");
            modelBuilder.Entity<UserHandingTakingOver>()
                .HasKey(k => k.Id)
                .Property(k => k.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            //modelBuilder.Entity<ApptDetails>().ToTable("AspNetAppointmentDetails");
            ////modelBuilder.Entity<ApptDetails>()
            ////    .HasKey(k => k.Id)
            ////    .Property(k => k.Id)
            ////    .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            #endregion

            #region Visit Mapping
            modelBuilder.Entity<Visit>().ToTable("AspNetVisit");
            modelBuilder.Entity<Visit>()
                .HasKey(k => k.Id)
                .Property(k => k.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Login>().ToTable("AspNetLogin");
            modelBuilder.Entity<Login>()
                .HasKey(k => k.Id)
                .Property(k => k.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            #endregion
        }

        #region Database Seed
        public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext db)
            {
                #region Seed
                if (!db.Users.Any())
                {
                    var roleStore = new RoleStore<ApplicationRole>(db);
                    var roleManager = new RoleManager<ApplicationRole>(roleStore);
                    var userStore = new UserStore<ApplicationUser>(db);
                    var userManager = new UserManager<ApplicationUser>(userStore);

                    var role1 = roleManager.FindByName("Administrator");
                    if (role1 == null)
                    {
                        role1 = new ApplicationRole("Administrator", "Admin", 0);
                        roleManager.Create(role1);
                    }
                    var role2 = roleManager.FindByName("User");
                    if (role2 == null)
                    {
                        role2 = new ApplicationRole("User", "Generic user with no previlleges. By default added to all users created.", 0);
                        roleManager.Create(role2);
                    }
                    var role3 = roleManager.FindByName("NoSubordinates");
                    if (role3 == null)
                    {
                        role3 = new ApplicationRole("NoSubordinates", "That user who cannot have subordinates.", 0);
                        roleManager.Create(role3);
                    }
                    // Create test users
                    var user = userManager.FindByName("admin");
                    if (user == null)
                    {
                        var newUser = new ApplicationUser()
                        {
                            UserName = Common.Configurations.Application.AdminUsername,
                            Email = string.Format("admin@{0}", Common.Configurations.Application.MailDomain),
                            EmailConfirmed = true,
                            Rank = "Brig",
                            PersonnelNumber = "******",
                            FullName = "Deputy Director General IT",
                            EstablishmentFull = Common.Configurations.Application.Name,
                            EstablishmentAbbreviation = Common.Configurations.Application.Abbreviation,
                            CreatedByIntId = -36920,
                            CreatedOn = DateTime.Now,
                            PhoneNumber = "39058",
                            MobNo = "1234",
                            IsPasswordHealthy = true
                        };
                        userManager.Create(newUser, Common.Configurations.Application.AdminPassword);
                        userManager.SetLockoutEnabled(newUser.Id, false);
                        userManager.AddToRole(newUser.Id, "Administrator");
                    }
                    #endregion
                    base.Seed(db);
                }
            }
        }
        #endregion
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name, string description, int IsPermission)
            : base(name)
        {
            this.Description = description;
            this.IsPermission = IsPermission;
        }

        public virtual string Description { get; set; }
        public virtual int IsPermission { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole
    {
        public ApplicationUserRole()
            : base()
        { }

        public ApplicationRole Role { get; set; }
    }

    public class UserHandingTakingOver
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public DateTime RequestDate { get; set; }
        public ApptDetails HandedOverBy { get; set; }
        public ApptDetails TakenOverBy { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
        public string ApprovingAuthority { get; set; }
    }

    public class ApptDetails
    {
        //public Guid Id { get; set; }
        [Required]
        [Display(Name = "Service Number")]
        public string ServiceNumber { get; set; }
        [Required]
        public string Rank { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Appointment { get; set; }
        public string MobNo { get; set; }
    }
}
