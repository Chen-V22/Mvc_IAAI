namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "AboutId", c => c.Int(nullable: false));
            CreateIndex("dbo.Positions", "AboutId");
            AddForeignKey("dbo.Positions", "AboutId", "dbo.Abouts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Positions", "AboutId", "dbo.Abouts");
            DropIndex("dbo.Positions", new[] { "AboutId" });
            DropColumn("dbo.Positions", "AboutId");
        }
    }
}
