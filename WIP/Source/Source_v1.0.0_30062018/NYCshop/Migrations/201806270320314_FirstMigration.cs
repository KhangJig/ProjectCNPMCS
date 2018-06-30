namespace NYCshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 30),
                        CategoryImage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 128),
                        ProductID = c.Int(nullable: false),
                        CommentContent = c.String(nullable: false),
                        TimeComment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.Username)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        Username = c.String(maxLength: 128),
                        SubCategoryID = c.Int(nullable: false),
                        ProductName = c.String(nullable: false),
                        UploadDate = c.DateTime(nullable: false),
                        Describe = c.String(nullable: false),
                        Quanlity = c.Int(nullable: false),
                        Price = c.Long(nullable: false),
                        SaleStatus = c.Boolean(nullable: false),
                        Censor = c.Boolean(nullable: false),
                        Viewed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.Username)
                .Index(t => t.SubCategoryID);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        SubCategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SubCategoryID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(),
                        Role = c.String(),
                        Address = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        JoiningDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        ErrorID = c.Int(nullable: false, identity: true),
                        ErrorContent = c.String(nullable: false),
                        FunctionName = c.String(nullable: false),
                        OccurDate = c.DateTime(nullable: false),
                        Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ErrorID)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.Username);
            
            CreateTable(
                "dbo.ImageUrls",
                c => new
                    {
                        ImageID = c.Int(nullable: false),
                        Url = c.String(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ReplyID = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 128),
                        CommentID = c.Int(nullable: false),
                        ReplyContent = c.String(nullable: false),
                        TimeReply = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReplyID)
                .ForeignKey("dbo.Comments", t => t.CommentID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.Username)
                .Index(t => t.CommentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Replies", "Username", "dbo.Users");
            DropForeignKey("dbo.Replies", "CommentID", "dbo.Comments");
            DropForeignKey("dbo.ImageUrls", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ErrorLogs", "Username", "dbo.Users");
            DropForeignKey("dbo.Comments", "Username", "dbo.Users");
            DropForeignKey("dbo.Comments", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "Username", "dbo.Users");
            DropForeignKey("dbo.Products", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Replies", new[] { "CommentID" });
            DropIndex("dbo.Replies", new[] { "Username" });
            DropIndex("dbo.ImageUrls", new[] { "ProductID" });
            DropIndex("dbo.ErrorLogs", new[] { "Username" });
            DropIndex("dbo.SubCategories", new[] { "CategoryID" });
            DropIndex("dbo.Products", new[] { "SubCategoryID" });
            DropIndex("dbo.Products", new[] { "Username" });
            DropIndex("dbo.Comments", new[] { "ProductID" });
            DropIndex("dbo.Comments", new[] { "Username" });
            DropTable("dbo.Replies");
            DropTable("dbo.ImageUrls");
            DropTable("dbo.ErrorLogs");
            DropTable("dbo.Users");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
        }
    }
}
