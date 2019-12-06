namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentyEighth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeHours", "StationPost_StationPostId", "dbo.StationPosts");
            DropIndex("dbo.TimeHours", new[] { "StationPost_StationPostId" });
            DropColumn("dbo.TimeHours", "StationPost_StationPostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeHours", "StationPost_StationPostId", c => c.Int());
            CreateIndex("dbo.TimeHours", "StationPost_StationPostId");
            AddForeignKey("dbo.TimeHours", "StationPost_StationPostId", "dbo.StationPosts", "StationPostId");
        }
    }
}
