using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_IA.Models
{
    public class DownLoad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "標題圖片")]
        [Required(ErrorMessage = "{0} 必填")]
        public string Photo { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "{0} 必填")]
        public string Title { get; set; }

        [Display(Name = "內容")]
        [Required(ErrorMessage = "{0} 必填")]
        public string Content { get; set; }

        [Display(Name = "置頂")]
        public IsTop IsTop { get; set; }

        [Display(Name = "點閱數")]
        public string Clicks { get; set; }

        [Display(Name = "發布日期")]
        public DateTime DateTime { get; set; }

    }
}