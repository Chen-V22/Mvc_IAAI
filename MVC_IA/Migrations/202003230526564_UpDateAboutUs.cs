namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpDateAboutUs : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Abouts", "PositionId", "dbo.Positions");
            //DropIndex("dbo.Abouts", new[] { "PositionId" });
            CreateTable(
                "dbo.AboutUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageList = c.String(nullable: false, maxLength: 100),
                        Clicks = c.String(nullable: false, maxLength: 10),
                        Announcer = c.String(nullable: false, maxLength: 10),
                        DateTime = c.DateTime(nullable: false),
                        UpDater = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            //DropTable("dbo.Abouts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Abouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gender = c.Int(nullable: false),
                        Experience = c.String(nullable: false, maxLength: 100),
                        Photo = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.AboutUs");
            CreateIndex("dbo.Abouts", "PositionId");
            AddForeignKey("dbo.Abouts", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
        }
    }
}
