using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AA7.Models
{
    [Table("FullHierarchy", Schema="dbo")]
    public class dbo_FullHierarchy
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "I D")]
        public Int64? ID { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public Int32 UserId { get; set; }

        [Required]
        [Display(Name = "Child Id")]
        public Int32 ChildId { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "User Name")]
        public String UserName { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Role Name")]
        public String RoleName { get; set; }

        [StringLength(4000)]
        [Display(Name = "Unit")]
        public String EstablishmentFull { get; set; }

    }
}
 
