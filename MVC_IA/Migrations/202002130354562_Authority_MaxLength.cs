namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authority_MaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Authority", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Authority", c => c.String());
        }
    }
}
