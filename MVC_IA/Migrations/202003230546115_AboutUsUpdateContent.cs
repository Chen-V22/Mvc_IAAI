namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AboutUsUpdateContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AboutUs", "Clicks", c => c.String(maxLength: 10));
            AlterColumn("dbo.AboutUs", "Announcer", c => c.String(maxLength: 10));
            AlterColumn("dbo.AboutUs", "UpDater", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AboutUs", "UpDater", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.AboutUs", "Announcer", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.AboutUs", "Clicks", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
