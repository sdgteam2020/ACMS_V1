using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHSDC.Common.Models
{
    /// <summary>
    /// <para>Defines Hierarchy Map of the application</para>
    /// </summary>
    public class Hierarchy
    {
        public Guid Id { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "Child Id")]
        public int ChildId { get; set; }
    }

    public class FullHierarchy
    {
        public Guid Id { get; set; }
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "Child Id")]
        public int ChildId { get; set; }
    }

    public class FullHierarchyView
    {
        [Display(Name = "User Id")]
        public int UserId { get; set; }
        [Display(Name = "Child Id")]
        public int ChildId { get; set; }
    }
}
