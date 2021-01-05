using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_IA.Models
{
    public class Forum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "標題")]
        [Required(ErrorMessage = "{0} 必填")]
        public string Title { get; set; }

        [Display(Name = "發布者")]
        [Required(ErrorMessage = "{0} 必填")]
        [MaxLength(8)]
        public string Name { get; set; }

        [Display(Name = "點閱數")]
        [MaxLength(10)]
        public string Clicks { get; set; }

        [Display(Name = "內容")]
        [Required(ErrorMessage = "{0} 必填")]
        public string Content { get; set; }

        [Display(Name = "發布時間")]
        public DateTime DateTime { get; set; }
    }
}