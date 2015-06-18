namespace BestBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrator",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        login = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        dateTime = c.DateTime(nullable: false),
                        name = c.String(),
                        email = c.String(),
                        body = c.String(),
                        post_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Post", t => t.post_ID)
                .Index(t => t.post_ID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        Title = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TagPost",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Post_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Post_ID })
                .ForeignKey("dbo.Tag", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Post", t => t.Post_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Post_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPost", "Post_ID", "dbo.Post");
            DropForeignKey("dbo.TagPost", "Tag_ID", "dbo.Tag");
            DropForeignKey("dbo.Comment", "post_ID", "dbo.Post");
            DropIndex("dbo.TagPost", new[] { "Post_ID" });
            DropIndex("dbo.TagPost", new[] { "Tag_ID" });
            DropIndex("dbo.Comment", new[] { "post_ID" });
            DropTable("dbo.TagPost");
            DropTable("dbo.Tag");
            DropTable("dbo.Post");
            DropTable("dbo.Comment");
            DropTable("dbo.Administrator");
        }
    }
}
