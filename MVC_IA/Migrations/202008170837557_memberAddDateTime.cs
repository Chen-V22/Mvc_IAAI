namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberAddDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "DateTime");
        }
    }
}
