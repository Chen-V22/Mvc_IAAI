namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAddForienkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Premissions", "User_Id", c => c.Int());
            CreateIndex("dbo.Premissions", "User_Id");
            AddForeignKey("dbo.Premissions", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Premissions", "User_Id", "dbo.Users");
            DropIndex("dbo.Premissions", new[] { "User_Id" });
            DropColumn("dbo.Premissions", "User_Id");
        }
    }
}
