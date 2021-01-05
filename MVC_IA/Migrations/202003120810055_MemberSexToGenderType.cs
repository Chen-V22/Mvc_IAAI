namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberSexToGenderType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.Members", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "Sex", c => c.String(nullable: false, maxLength: 2));
            DropColumn("dbo.Members", "Gender");
        }
    }
}
