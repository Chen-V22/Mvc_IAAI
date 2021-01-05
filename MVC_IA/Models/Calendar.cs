using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_IA.Models
{
    public class Calendar
    {
        [Key]
        [ DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [ Display(Name = "網頁列表")]
        [ MaxLength(100)]
        public string PageList { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [ Display(Name = "內容")]
        public string Content { get; set; }

        [Display(Name = "點閱數")]
        [ MaxLength(10)]
        public string Clicks { get; set; }

        [Display(Name = "發布者")]
        [ MaxLength(10)]
        public string Announcer { get; set; }

        [Display(Name = "發布時間")]
        public DateTime DateTime { get; set; }

        [Display(Name = "更新者")]
        [ MaxLength(10)]
        public string UpDater { get; set; }
    }
}