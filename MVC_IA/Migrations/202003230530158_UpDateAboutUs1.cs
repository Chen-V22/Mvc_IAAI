namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpDateAboutUs1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Positions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PositionName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
