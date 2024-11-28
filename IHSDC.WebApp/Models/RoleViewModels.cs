using IHSDC.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IHSDC.WebApp.Models
{
    public class RoleViewModel
    {
        [Display(Name = "Role Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Only alphanumeric characters allowed")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        [Display(Name = "Permission Type")]
        public int IsPermission { get; set; }
        public string Id { get; set; }
        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role)
        {
            this.Id = role.Id;
            this.RoleName = role.Name;
            this.Description = role.Description;
            this.IsPermission = role.IsPermission;
        }
    }

    public class SelectRoleEditorViewModel
    {
        public SelectRoleEditorViewModel() { }

        public SelectRoleEditorViewModel(ApplicationRole role)
        {
            this.RoleName = role.Name;
            this.Description = role.Description;
        }

        public bool Selected { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
    }

    public class EditRoleViewModel
    {
        [Display(Name = "Original Role Name")]
        public string OriginalRoleName { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public EditRoleViewModel() { }
        public EditRoleViewModel(ApplicationRole role)
        {
            this.OriginalRoleName = role.Name;
            this.RoleName = role.Name;
            this.Description = role.Description;
            this.Id = role.Id;
        }
    }
}