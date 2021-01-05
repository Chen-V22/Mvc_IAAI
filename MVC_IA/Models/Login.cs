using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MVC_IA.Models
{
    public class Login
    {
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
    }
}