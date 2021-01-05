namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Premission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Premissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        pid = c.Int(),
                        PValue = c.String(nullable: false),
                        Url = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Premissions", t => t.pid)
                .Index(t => t.pid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Premissions", "pid", "dbo.Premissions");
            DropIndex("dbo.Premissions", new[] { "pid" });
            DropTable("dbo.Premissions");
        }
    }
}
