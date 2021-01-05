namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactUsEditMaxLength50 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactUs", "Email", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactUs", "Email", c => c.String(nullable: false, maxLength: 8));
        }
    }
}
