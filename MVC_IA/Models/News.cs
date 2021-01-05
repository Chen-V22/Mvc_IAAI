using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_IA.Models
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "圖片")]
        [MaxLength(50)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "名稱")]
        [MaxLength(60)]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "簡介")]
        [MaxLength(120)]
        public string Introduction { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "內容")]
        public string Content { get; set; }

        [Display(Name = "是否置頂")]
        public IsTop Top { get; set; }

        [Display(Name = "時間")]
        public DateTime Date { get; set; }
    }
}