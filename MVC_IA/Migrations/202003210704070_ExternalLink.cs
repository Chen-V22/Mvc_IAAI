namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExternalLink : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExternalLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Picture = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Url = c.String(nullable: false),
                        IsTop = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExternalLinks");
        }
    }
}
