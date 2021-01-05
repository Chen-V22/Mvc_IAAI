using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_IA.Models
{
    public class HomePicture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "首頁圖片")]
        public  string Picture { get; set; }

        [Display(Name = "置頂")]
        public  IsTop IsTop { get; set; }

        [Display(Name = "順序排列")]
        public  int Sort { get; set; }

        [Display(Name = "上傳日期")]
        public  DateTime DateTime { get; set; }
    }
}