namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DownLoad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DownLoads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        IsTop = c.Int(nullable: false),
                        Clicks = c.String(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DownLoads");
        }
    }
}
