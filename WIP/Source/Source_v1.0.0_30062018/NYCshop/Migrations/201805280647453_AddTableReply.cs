namespace NYCshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableReply : DbMigration
    {
        public override void Up()
        {
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
            DropIndex("dbo.Replies", new[] { "CommentID" });
            DropIndex("dbo.Replies", new[] { "Username" });
            DropTable("dbo.Replies");
        }
    }
}
