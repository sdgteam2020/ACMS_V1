using IHSDC.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IHSDC.WebApp.Models
{
    public class HandingTakingOverListViewModel
    {
        public string Username { get; set; }
        public DateTime RequestDate { get; set; }
        [Required]
        [Display(Name = "Handining Over By")]
        public ApptDetails HandedOverBy { get; set; }
        [Required]
        [Display(Name = "Taking Over By")]
        public ApptDetails TakenOverBy { get; set; }
        public string Reason { get; set; }
        public string ApproveAction { get; set; }
    }

    public class UserHandingTakingOverViewModel
    {
        public DateTime RequestDate { get; set; }
        [Required]
        [Display(Name = "Taking Over By")]
        public ApptDetails TakenOverBy { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
    }

    public class ApptDetailsViewModel
    {
        [Required]
        [Display(Name = "Service Number")]
        public string ServiceNumber { get; set; }
        [Required]
        public string Rank { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Appointment { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string IpAddress { get; set; }
    }

    public class CheckUser
    {
        public String UserName { get; set; }
        public string userTime { get; set; }
        public IList<CheckUser> ILCheckUser { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username/Domain Id")]
        [RegularExpression("^[a-zA-Z0-9 _ ]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "DE Fwd Auth")]
        public string DEFwdAuth { get; set; }


        [Display(Name = "GEB Fwd Auth")]
        public string GEBFwdAuth { get; set; }

        [Display(Name = "Status")]
        public string Active { get; set; }

        [Display(Name = "Unit / Fmn/ Dte")]
        public string EstablishmentFull { get; set; }

        [Display(Name = "Unit_ID")]
        public int Unit_ID { get; set; }

        // [RegularExpression("^[a-zA-Z0-9-&)(  ]*$", ErrorMessage = "Only Alphabets, Numbers,AND and brackets allowed.")]
        [Display(Name = "Unit Name")]
        public string EstablishmentAbbreviation { get; set; }

        [Required]
        public string Appointment { get; set; }

        [Required]
        [Display(Name = "Pers Number")]
        //  [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Number { get; set; }

        [Required]
        public string Rank { get; set; }

        [Required]
        // [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        // [RegularExpression(@"^[0-9]+$", ErrorMessage = "ASCON No can only be numbers.")]
        [Display(Name = "ASCON Number")]
        public string ASCON { get; set; }

        [Required]
        [Display(Name = "Roles")]
        public string[] Roles { get; set; }

        [Display(Name = "UserType")]
        public int UserTypeId { get; set; }
        [Required]
        [Display(Name = "ParentId")]
        public int ParentId { get; set; }
        public int CreatedByIntId { get; set; }
        public ApplicationUser Superior { get; set; }
        public int ComdId { get; set; }

        public int ComdIdSignUp { get; set; }
        public int CorpId { get; set; }
        public int BdeId { get; set; }

        [Required]
        [Display(Name = "Mob No")]
        public string MobNo { get; set; }
    }


    public class AddSuperiorViewModel
    {
        [Required]
        [Display(Name = "Select Subordinate")]
        public string SubordinateId { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "DE Fwd Auth")]
        public string DEFwdAuth { get; set; }


        [Display(Name = "GEB Fwd Auth")]
        public string GEBFwdAuth { get; set; }

        [Display(Name = "Status")]
        public string Active { get; set; }

        [Display(Name = "Unit / Fmn/ Dte")]
        public string EstablishmentFull { get; set; }

        [Required]
        [Display(Name = "Unit Name")]
        public string EstablishmentAbbreviation { get; set; }

        [Required]
        public string Appointment { get; set; }

        [Required]
        [Display(Name = "Mob No")]
        public string MobNo { get; set; }


        [Required]
        [Display(Name = "Pers Number")]
        public string Number { get; set; }

        [Required]
        public string Rank { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers allowed")]
        [Display(Name = "ASCON Number")]
        public string ASCON { get; set; }

        [Required]
        [Display(Name = "Roles")]
        public string[] Roles { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "required!")]

        public string Email { get; set; }
        public int id { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int ChildId { get; set; }
        public string superiorId { get; set; }
        public string createTime { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string RoleName { get; set; }



        public List<ForgotPasswordViewModel> ilForgotPasswordViewModel { get; set; }



    }

    public class CheckLoggedIn
    {
        public bool LoggedIn { get; set; }
        public int IntId { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool IsSuccess { get; set; }

    }




    public class EditUserViewModel
    {
        
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "UserId")]
        public string UserId { get; set; }

        [Display(Name = "DE Fwd Auth")]
        public string DEFwdAuth { get; set; }


        [Display(Name = "GEB Fwd Auth")]
        public string GEBFwdAuth { get; set; }

        [Display(Name = "Status")]
        public string Active { get; set; }

        [Display(Name = "Unit / Fmn/ Dte")]
        public string EstablishmentFull { get; set; }
        //[Display(Name = "Unit_ID")]
        //public int Unit_ID { get; set; }

        [Display(Name = "Unit Name")]
        public string EstablishmentAbbreviation { get; set; }

        [Required]
        public string Appointment { get; set; }

        [Required]
        [Display(Name = "Pers Number")]
        public string Number { get; set; }

        [Required]
        public string Rank { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers allowed")]
        [Display(Name = "ASCON Number")]
        public string ASCON { get; set; }


        [Display(Name = "Unit_ID")]
        public int Unit_ID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required]
        //[Display(Name = "Roles")]
        //public string[] Roles { get; set; }
        //[Required]
        //[Display(Name = "UserType")]
        //public int UserTypeId { get; set; }
        //[Required]
        //[Display(Name = "ParentId")]
        //public int ParentId { get; set; }
        [Required]
        [Display(Name = "UserType")]
        public int UserTypeId { get; set; }
        [Required]
        [Display(Name = "Roles")]
        public string[] Roles { get; set; }
        public int ComdId { get; set; }
        public int CorpId { get; set; }
        public int BdeId { get; set; }

        [Required]
        [Display(Name = "Mob No")]
        public string MobNo { get; set; }

        public EditUserViewModel() { }
        public EditUserViewModel(ApplicationUser user)
        {
            this.Username = user.UserName;
            this.EstablishmentFull = user.EstablishmentFull;
            this.DEFwdAuth = user.DEFwdAuth;
            this.GEBFwdAuth = user.GEBFwdAuth;
            this.Active = user.Active;
            this.EstablishmentAbbreviation = user.EstablishmentAbbreviation;
            this.Appointment = user.Appointment;
            
            this.Number = user.PersonnelNumber;
            this.Rank = user.Rank;
            this.FullName = user.FullName;
            this.ASCON = user.PhoneNumber;
            this.UserTypeId = user.UserTypeId;
            this.Unit_ID = user.Unit_ID;
            this.MobNo = user.MobNo;

        }
    }

    public class DeletetUserViewModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; }


        [Display(Name = "DE Fwd Auth")]
        public string DEFwdAuth { get; set; }


        [Display(Name = "GEB Fwd Auth")]
        public string GEBFwdAuth { get; set; }

        [Display(Name = "Status")]
        public string Active { get; set; }

        [Display(Name = "Unit / Fmn/ Dte")]
        public string EstablishmentFull { get; set; }

        [Required]
        [Display(Name = "Unit Name")]
        public string EstablishmentAbbreviation { get; set; }

        [Required]
        public string Appointment { get; set; }
        
        [Required]
        [Display(Name = "Mob No")]
        public string MobNo { get; set; }

        [Required]
        [Display(Name = "Pers Number")]
        public string Number { get; set; }

        [Required]
        public string Rank { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public string SuperiorEstablishment { get; set; }
        public IEnumerable<string> SubordinateEstablishments { get; set; }

        public DeletetUserViewModel() { }
        public DeletetUserViewModel(ApplicationUser user)
        {
            this.Username = user.UserName;
            this.DEFwdAuth = user.DEFwdAuth;
            this.GEBFwdAuth = user.GEBFwdAuth;
            this.Active = user.Active;
            this.EstablishmentFull = user.EstablishmentFull;
            this.EstablishmentAbbreviation = user.EstablishmentAbbreviation;
            this.Appointment = user.Appointment;
            this.MobNo = user.MobNo;
            this.Number = user.PersonnelNumber;
            this.Rank = user.Rank;
            this.FullName = user.FullName;
            this.SuperiorEstablishment = this.GetSuperior(user);
            this.SubordinateEstablishments = this.GetSubordinateUsernames(user.Subordinates);
        }

        private string GetSuperior(ApplicationUser user)
        {
            return string.IsNullOrEmpty(user.EstablishmentFull) ? "Superuser" : user.Superior.EstablishmentFull.ToString();
        }

        private IEnumerable<string> GetSubordinateUsernames(ICollection<ApplicationUser> subordinates)
        {
            var returnStr = new List<string>();
            if (subordinates.Count < 1)
            {
                returnStr.Add("No Subordinates");
                return returnStr;
            }
            foreach (var subordinate in subordinates)
            {
                returnStr.Add(subordinate.EstablishmentFull);
            }
            return returnStr;
        }
    }
}
