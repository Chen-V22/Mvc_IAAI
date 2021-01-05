namespace MVC_IA.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=ISAIAH")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public System.Data.Entity.DbSet<MVC_IA.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.News> News { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.Member> Members { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.HomePicture> HomePictures { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.ExternalLink> ExternalLinks { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.ContactUs> ContactUs { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.Forum> Fora { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.DownLoad> DownLoads { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.AboutUs> AboutUs { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.Association> Associations { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.Calendar> Calendars { get; set; }

        public System.Data.Entity.DbSet<MVC_IA.Models.Premission> Premissions { get; set; }
    }
}
