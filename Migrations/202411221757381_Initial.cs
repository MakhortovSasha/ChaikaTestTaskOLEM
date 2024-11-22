namespace Chaika_TestTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionIcon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BackgroundColor = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionTypeProperty = c.Int(nullable: false),
                        Summ = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        AuthorizedUser_Id = c.Int(),
                        Icon_ID = c.Int(),
                        RelatedUser_Id = c.Int(nullable: false),
                        TransactionIcon_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorizedUser_Id)
                .ForeignKey("dbo.TransactionIcon", t => t.Icon_ID)
                .ForeignKey("dbo.Users", t => t.RelatedUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.TransactionIcon", t => t.TransactionIcon_ID)
                .Index(t => t.AuthorizedUser_Id)
                .Index(t => t.Icon_ID)
                .Index(t => t.RelatedUser_Id)
                .Index(t => t.TransactionIcon_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "TransactionIcon_ID", "dbo.TransactionIcon");
            DropForeignKey("dbo.Transaction", "RelatedUser_Id", "dbo.Users");
            DropForeignKey("dbo.Transaction", "Icon_ID", "dbo.TransactionIcon");
            DropForeignKey("dbo.Transaction", "AuthorizedUser_Id", "dbo.Users");
            DropIndex("dbo.Transaction", new[] { "TransactionIcon_ID" });
            DropIndex("dbo.Transaction", new[] { "RelatedUser_Id" });
            DropIndex("dbo.Transaction", new[] { "Icon_ID" });
            DropIndex("dbo.Transaction", new[] { "AuthorizedUser_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Transaction");
            DropTable("dbo.TransactionIcon");
        }
    }
}
