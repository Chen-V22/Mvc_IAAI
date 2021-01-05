namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Calendar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calendars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageList = c.String(nullable: false, maxLength: 100),
                        Content = c.String(nullable: false),
                        Clicks = c.String(maxLength: 10),
                        Announcer = c.String(maxLength: 10),
                        DateTime = c.DateTime(nullable: false),
                        UpDater = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Calendars");
        }
    }
}
