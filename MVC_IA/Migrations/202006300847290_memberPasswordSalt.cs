namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberPasswordSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "PasswordSalt", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "PasswordSalt");
        }
    }
}
