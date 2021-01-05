namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AboutUsAddContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AboutUs", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AboutUs", "Content");
        }
    }
}
