using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MVC_IA.Migrations;

namespace MVC_IA.Models
{
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "編號")]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "姓名")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "帳號")]
        [MaxLength(50)]
        public string Account { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [MaxLength(100)]
        [Display(Name = "密碼鹽")]
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "性別")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "生日")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "會員種類")]
        public MemberKind MemberKind { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "通訊地址")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "E-mail")]
        [MaxLength(100)]
        public string Email { get; set; }

        [Display(Name = "國際會員")]
        [MaxLength(5)]
        public string GlobalMember { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "現職單位")]
        [MaxLength(50)]
        public string WorkDepartment { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "職稱")]
        [MaxLength(30)]
        public string WorkPosition { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "學歷")]
        [MaxLength(30)]
        public string Education { get; set; }

        [Display(Name = "服務單位")]
        [MaxLength(50)]
        public string ServiceDepartment { get; set; }

        [Display(Name = "職稱")]
        [MaxLength(30)]
        public string ServicePosition { get; set; }

        [Display(Name = "起")]
        [MaxLength(10)]
        public string DateStart { get; set; }

        [Display(Name = "迄")]
        [MaxLength(10)]
        public string DateEnd { get; set; }

        [Display(Name = "服務單位")]
        [MaxLength(50)]
        public string SecServiceDepartment { get; set; }

        [Display(Name = "職稱")]
        [MaxLength(30)]
        public string SecServicePosition { get; set; }

        [Display(Name = "起")]
        [MaxLength(10)]
        public string SecDateStart { get; set; }

        [Display(Name = "迄")]
        [MaxLength(10)]
        public string SecDateEnd { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "相關年資合計")]
        [MaxLength(10)]
        public string DateTotal { get; set; }

        [Display(Name = "註冊日期")]
        public DateTime DateTime { get; set; }
    }
}