namespace NYCshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableWishList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WishLists", "Username", "dbo.Users");
            DropPrimaryKey("dbo.WishLists");
            AddPrimaryKey("dbo.WishLists", new[] { "Username", "ProductID" });
            AddForeignKey("dbo.WishLists", "Username", "dbo.Users", "Username", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "Username", "dbo.Users");
            DropPrimaryKey("dbo.WishLists");
            AddPrimaryKey("dbo.WishLists", "Username");
            AddForeignKey("dbo.WishLists", "Username", "dbo.Users", "Username");
        }
    }
}
