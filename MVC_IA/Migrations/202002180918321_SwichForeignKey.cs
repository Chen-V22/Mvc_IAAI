namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SwichForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Positions", "AboutId", "dbo.Abouts");
            DropIndex("dbo.Positions", new[] { "AboutId" });
            AddColumn("dbo.Abouts", "PositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Abouts", "PositionId");
            AddForeignKey("dbo.Abouts", "PositionId", "dbo.Positions", "Id", cascadeDelete: true);
            DropColumn("dbo.Positions", "AboutId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "AboutId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Abouts", "PositionId", "dbo.Positions");
            DropIndex("dbo.Abouts", new[] { "PositionId" });
            DropColumn("dbo.Abouts", "PositionId");
            CreateIndex("dbo.Positions", "AboutId");
            AddForeignKey("dbo.Positions", "AboutId", "dbo.Abouts", "Id", cascadeDelete: true);
        }
    }
}
