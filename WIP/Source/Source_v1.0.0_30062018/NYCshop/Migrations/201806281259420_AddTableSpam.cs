namespace NYCshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableSpam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Spams",
                c => new
                    {
                        SpamID = c.Int(nullable: false, identity: true),
                        Reporter = c.String(maxLength: 128),
                        SpamContent = c.String(nullable: false),
                        ProductID = c.Int(nullable: false),
                        ReportDate = c.DateTime(nullable: false),
                        Resolved = c.Boolean(nullable: false),
                        ResolveDate = c.DateTime(),
                        Resolver = c.String(maxLength: 128),
                        ProperReport = c.Boolean(),
                    })
                .PrimaryKey(t => t.SpamID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Reporter)
                .ForeignKey("dbo.Users", t => t.Resolver)
                .Index(t => t.Reporter)
                .Index(t => t.ProductID)
                .Index(t => t.Resolver);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Spams", "Resolver", "dbo.Users");
            DropForeignKey("dbo.Spams", "Reporter", "dbo.Users");
            DropForeignKey("dbo.Spams", "ProductID", "dbo.Products");
            DropIndex("dbo.Spams", new[] { "Resolver" });
            DropIndex("dbo.Spams", new[] { "ProductID" });
            DropIndex("dbo.Spams", new[] { "Reporter" });           
            DropTable("dbo.Spams");            
        }
    }
}
