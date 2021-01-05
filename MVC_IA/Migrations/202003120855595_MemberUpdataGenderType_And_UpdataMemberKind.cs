namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberUpdataGenderType_And_UpdataMemberKind : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "MemberKind", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "MemberKind", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
