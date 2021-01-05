using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MVC_IA.Models
{
    public class User
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
        [Display(Name = "照片")]
        [MaxLength(50)]
        public string Photo { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "帳號(E-mail)")]
        [JsonProperty]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} 必填")]
        [Display(Name = "密碼")]
        [MaxLength(Int32.MaxValue)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MaxLength(100)]
        [Display(Name = "密碼鹽")]
        public string PasswordSalt { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "時間")]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "權限")]
        [MaxLength(100)]
        public string Authority { get; set; }

        public virtual ICollection<Premission> pid { get; set; }
    }
}