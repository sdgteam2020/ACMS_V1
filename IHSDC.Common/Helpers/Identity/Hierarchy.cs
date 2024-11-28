using IHSDC.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHSDC.Common.Helpers.Identity
{
    public class Hierarchy
    {
        public static List<ApplicationUser> GetHierarchyUsers(ApplicationUser CurrentUser)
        {
            var response = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = CurrentUser.Id,
                    UserName = CurrentUser.UserName,
                    EstablishmentAbbreviation = CurrentUser.EstablishmentAbbreviation,
                    EstablishmentFull = CurrentUser.EstablishmentFull,
                    DEFwdAuth = CurrentUser.DEFwdAuth,
                    GEBFwdAuth = CurrentUser.GEBFwdAuth,
                    Active = CurrentUser.Active,
                    Rank = CurrentUser.Rank,
                    FullName = CurrentUser.FullName,
                    IntId = CurrentUser.IntId,
                    Superior = CurrentUser.Superior,
                    SuperiorId = CurrentUser.SuperiorId,
                    Subordinates = CurrentUser.Subordinates,
                    CreatedByIntId = CurrentUser.CreatedByIntId,
                    CreatedOn = CurrentUser.CreatedOn,
                    Email = CurrentUser.Email,
                    PhoneNumber = CurrentUser.PhoneNumber,
                    PersonnelNumber = CurrentUser.PersonnelNumber,
                    Appointment = CurrentUser.Appointment,
                    Unit_ID = CurrentUser.Unit_ID,
                    MobNo= CurrentUser.MobNo
                }
            };
            var AllSubordinatesToCurrentUser = CurrentUser.Subordinates;
            foreach (var SubordinateToCurrentUser in AllSubordinatesToCurrentUser)
            {
                var SubordinateOfSubordinateToCurrentUser = GetHierarchyUsers(SubordinateToCurrentUser);
                foreach (var User in SubordinateOfSubordinateToCurrentUser) response.Add(User);
            }
            return response;
        }

        public static List<ApplicationUser> GetHierarchy(ApplicationUser CurrentUser)
        {
            var response = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = CurrentUser.Id,
                    UserName = CurrentUser.UserName,
                    EstablishmentAbbreviation = CurrentUser.EstablishmentAbbreviation,
                    EstablishmentFull = CurrentUser.EstablishmentFull,
                    DEFwdAuth = CurrentUser.DEFwdAuth,
                    GEBFwdAuth = CurrentUser.GEBFwdAuth,
                    Active = CurrentUser.Active,
                    Rank = CurrentUser.Rank,
                    FullName = CurrentUser.FullName,
                    IntId = CurrentUser.IntId,
                    Superior = CurrentUser.Superior,
                    SuperiorId = CurrentUser.SuperiorId,
                    Subordinates = CurrentUser.Subordinates,
                    CreatedByIntId = CurrentUser.CreatedByIntId,
                    CreatedOn = CurrentUser.CreatedOn,
                    Email = CurrentUser.Email,
                    PhoneNumber = CurrentUser.PhoneNumber,
                    PersonnelNumber = CurrentUser.PersonnelNumber,
                    MobNo= CurrentUser.MobNo,
                    Appointment = CurrentUser.Appointment
                }
            };
            var AllSubordinatesToCurrentUser = CurrentUser.Subordinates;
            foreach (var SubordinateToCurrentUser in AllSubordinatesToCurrentUser)
            {
                var SubordinateOfSubordinateToCurrentUser = GetHierarchy(SubordinateToCurrentUser);
                foreach (var User in SubordinateOfSubordinateToCurrentUser) response.Add(User);
            }
            return response;
        }

        public static bool UpdateHierarchyTable()
        {
            using (var db = new ApplicationDbContext())
            {
                if (db.Users.Count() == 1) return false;
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [AspNetFullHierarchy]");
                var users = db.Users.ToList().OrderBy(u => u.IntId);
                foreach (var user in users)
                {
                    var HierarchyList = GetHierarchy(user);
                    foreach (var item in HierarchyList)
                    {
                        if (user.IntId != item.IntId)
                        {
                            db.FullHierarchy.Add(
                                new FullHierarchy
                                {
                                    UserId = user.IntId,
                                    ChildId = item.IntId
                                });
                        }
                        else
                        {
                            //Children Getting Added Here
                            db.FullHierarchy.Add(
                                new FullHierarchy
                                {
                                    UserId = user.IntId,
                                    ChildId = 0
                                });
                        }
                    }
                }
                db.SaveChanges();
                return true;
            }
        }
    }

   
}
