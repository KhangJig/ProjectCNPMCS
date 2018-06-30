namespace NYCshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableErrorLog : DbMigration
    {
        public override void Up()
        {
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
        }
        
        public override void Down()
        {
            DropIndex("dbo.ErrorLogs", new[] { "Username" });
            DropTable("dbo.ErrorLogs");
        }
    }
}
