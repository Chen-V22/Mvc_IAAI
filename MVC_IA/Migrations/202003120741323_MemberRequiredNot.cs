namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberRequiredNot : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "ServiceDepartment", c => c.String(maxLength: 50));
            AlterColumn("dbo.Members", "ServicePosition", c => c.String(maxLength: 30));
            AlterColumn("dbo.Members", "DateStart", c => c.String(maxLength: 10));
            AlterColumn("dbo.Members", "DateEnd", c => c.String(maxLength: 10));
            AlterColumn("dbo.Members", "SecServiceDepartment", c => c.String(maxLength: 50));
            AlterColumn("dbo.Members", "SecServicePosition", c => c.String(maxLength: 30));
            AlterColumn("dbo.Members", "SecDateStart", c => c.String(maxLength: 10));
            AlterColumn("dbo.Members", "SecDateEnd", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "SecDateEnd", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Members", "SecDateStart", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Members", "SecServicePosition", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Members", "SecServiceDepartment", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Members", "DateEnd", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Members", "DateStart", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Members", "ServicePosition", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Members", "ServiceDepartment", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
