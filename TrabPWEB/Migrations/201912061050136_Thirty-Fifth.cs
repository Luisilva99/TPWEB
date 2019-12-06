namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtyFifth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeHours", "TimeHourId", "dbo.StationPosts");
            DropIndex("dbo.TimeHours", new[] { "TimeHourId" });
            AddColumn("dbo.TimeHours", "StationPost_StationPostId", c => c.Int());
            CreateIndex("dbo.TimeHours", "StationPost_StationPostId");
            AddForeignKey("dbo.TimeHours", "StationPost_StationPostId", "dbo.StationPosts", "StationPostId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeHours", "StationPost_StationPostId", "dbo.StationPosts");
            DropIndex("dbo.TimeHours", new[] { "StationPost_StationPostId" });
            DropColumn("dbo.TimeHours", "StationPost_StationPostId");
            CreateIndex("dbo.TimeHours", "TimeHourId");
            AddForeignKey("dbo.TimeHours", "TimeHourId", "dbo.StationPosts", "StationPostId");
        }
    }
}
