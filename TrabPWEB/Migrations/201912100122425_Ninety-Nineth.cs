namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinetyNineth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StationPostsAtribuitions",
                c => new
                    {
                        StationPostsAtribuitionId = c.Int(nullable: false, identity: true),
                        StationPostId = c.Int(nullable: false),
                        StationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StationPostsAtribuitionId)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .ForeignKey("dbo.StationPosts", t => t.StationPostId, cascadeDelete: true)
                .Index(t => t.StationPostId)
                .Index(t => t.StationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StationPostsAtribuitions", "StationPostId", "dbo.StationPosts");
            DropForeignKey("dbo.StationPostsAtribuitions", "StationId", "dbo.Stations");
            DropIndex("dbo.StationPostsAtribuitions", new[] { "StationId" });
            DropIndex("dbo.StationPostsAtribuitions", new[] { "StationPostId" });
            DropTable("dbo.StationPostsAtribuitions");
        }
    }
}
