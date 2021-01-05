namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DownLoadClicksEdit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DownLoads", "Clicks", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DownLoads", "Clicks", c => c.String(nullable: false));
        }
    }
}
