namespace MVC_IA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Member : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Account = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Sex = c.String(nullable: false, maxLength: 2),
                        Birthday = c.String(nullable: false, maxLength: 10),
                        MemberKind = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        GlobalMember = c.String(maxLength: 5),
                        WorkDepartment = c.String(nullable: false, maxLength: 50),
                        WorkPosition = c.String(nullable: false, maxLength: 30),
                        Education = c.String(nullable: false, maxLength: 30),
                        ServiceDepartment = c.String(nullable: false, maxLength: 50),
                        ServicePosition = c.String(nullable: false, maxLength: 30),
                        DateStart = c.String(nullable: false, maxLength: 10),
                        DateEnd = c.String(nullable: false, maxLength: 10),
                        SecServiceDepartment = c.String(nullable: false, maxLength: 50),
                        SecServicePosition = c.String(nullable: false, maxLength: 30),
                        SecDateStart = c.String(nullable: false, maxLength: 10),
                        SecDateEnd = c.String(nullable: false, maxLength: 10),
                        DateTotal = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Members");
        }
    }
}
