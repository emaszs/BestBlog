namespace BestBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataLimits : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comment", "name", c => c.String(maxLength: 25));
            AlterColumn("dbo.Comment", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Comment", "body", c => c.String(maxLength: 200));
            AlterColumn("dbo.Post", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.Post", "Body", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Tag", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tag", "Name", c => c.String());
            AlterColumn("dbo.Post", "Body", c => c.String());
            AlterColumn("dbo.Post", "Title", c => c.String());
            AlterColumn("dbo.Comment", "body", c => c.String());
            AlterColumn("dbo.Comment", "email", c => c.String());
            AlterColumn("dbo.Comment", "name", c => c.String());
        }
    }
}
