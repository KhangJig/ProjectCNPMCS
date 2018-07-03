namespace NYCshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableWishList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Username)
                .Index(t => t.Username)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "Username", "dbo.Users");
            DropForeignKey("dbo.WishLists", "ProductID", "dbo.Products");
            DropIndex("dbo.WishLists", new[] { "ProductID" });
            DropIndex("dbo.WishLists", new[] { "Username" });
            DropTable("dbo.WishLists");
        }
    }
}
