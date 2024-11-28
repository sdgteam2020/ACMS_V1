using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AA7.Models
{
    [Table("tbl_Unit", Schema="dbo")]
    public class dbo_tbl_Unit
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Unit I D")]
        public Int32 Unit_ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Unit Name")]
        public String UnitName { get; set; }

         [Required]
       
        [Display(Name = "Command")]
        public Int32 Command { get; set; }

        [Required]
       
        [Display(Name = "Corps")]
        public Int32 Corps { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Type Of Unit")]
        public String TypeOfUnit { get; set; }

        [Required]
        [Display(Name = "Updated By User I D")]
        public Int32 UpdatedByUserID { get; set; }

        [Required]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [Display(Name = "Date Time of Update")]
        public DateTime DateTimeOfUpdate { get; set; }


    }






}
 
