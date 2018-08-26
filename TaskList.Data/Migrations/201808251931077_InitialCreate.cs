namespace TaskList.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        TimeLeft = c.Int(nullable: false),
                        Complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Department = c.String(nullable: false),
                        Username = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 15),
                        ConfirmPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
        }
    }
}
