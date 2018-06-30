namespace NYCshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        CommentContent = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.Username)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Username", "dbo.Users");
            DropForeignKey("dbo.Comments", "ProductID", "dbo.Products");
            DropIndex("dbo.Comments", new[] { "ProductID" });
            DropIndex("dbo.Comments", new[] { "Username" });
            DropTable("dbo.Comments");
        }
    }
}
