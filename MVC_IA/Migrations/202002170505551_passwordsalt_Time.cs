namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class passwordsalt_Time : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordSalt", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Date");
            DropColumn("dbo.Users", "PasswordSalt");
        }
    }
}
