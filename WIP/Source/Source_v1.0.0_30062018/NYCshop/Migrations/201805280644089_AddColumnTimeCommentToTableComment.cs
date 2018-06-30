namespace NYCshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnTimeCommentToTableComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "TimeComment", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "TimeComment");
        }
    }
}
