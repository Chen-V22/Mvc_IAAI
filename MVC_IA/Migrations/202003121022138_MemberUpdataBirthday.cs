namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberUpdataBirthday : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Birthday", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
