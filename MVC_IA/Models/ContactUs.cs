using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_IA.Models
{
    public class ContactUs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0} 必填")]
        [MaxLength(8)]
        public string Name { get; set; }

        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Display(Name = "電話")]
        [ Required(ErrorMessage = "{0} 必填")]
        [ MaxLength(25)]
        public string Phone { get; set; }

        [Display(Name = "E-mail")]
        [ Required(ErrorMessage = "{0} 必填")]
        [ MaxLength(50)]
        public string Email { get; set; }

        [Display(Name = "標題")]
        [ Required(ErrorMessage = "{0} 必填")]
        public string Title { get; set; }

        [Display(Name = "詢問內容")]
        [ Required(ErrorMessage = "{0} 必填")]
        public string Content { get; set; }

        [Display(Name = "詢問日期")]
        public DateTime DateTime { get; set; }
    }
}