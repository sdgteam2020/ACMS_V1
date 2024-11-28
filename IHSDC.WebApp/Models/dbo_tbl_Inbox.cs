using IHSDC.WebApp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ACC.Models
{
    [Table("tbl_Inbox", Schema="dbo")]
    public class dbo_tbl_Inbox
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Inbox ID")]
        public Int32 Inbox_ID { get; set; }

        [Required]
        [Display(Name = "Schedule ID")]
        public Int32 Schedule_ID { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Title")]
        public String Title { get; set; }
        
        [Required]
        [Display(Name = "Summary")]
        public String Summary { get; set; }

        [Required]
        [Display(Name = "Upload")]
        public String Upload { get; set; }


        [Display(Name = "Updated By User ID")]
        public Int32? UpdatedByUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Updated Date Time")]
        public DateTime? UpdatedDateTime { get; set; }

        public virtual ScheduleLetter ScheduleLetter { get; set; }
    }
}
 
