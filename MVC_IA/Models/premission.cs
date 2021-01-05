using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_IA.Models
{
    public class Premission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "名稱")]
        [MaxLength(50)]
        [Required(ErrorMessage = "必填{0}")]
        public string Name { get; set; }

        [Display(Name = "")]
        public int? pid { get; set; }

        //關聯Premissions
        [Display(Name = "父權限")]
        [ForeignKey("pid")]
        public Premission Premissions { get; set; }

        [Display(Name = "子權限")]
        [MaxLength(50)]
        public virtual ICollection<Premission> premissionSon { get; set; }

        [Display(Name = "權限代號")]
        [Required(ErrorMessage = "必填{0}")]
        public string PValue { get; set; }

        [Display(Name = "連結")]
        [MaxLength(50)]
        public string Url { get; set; }
    }
}