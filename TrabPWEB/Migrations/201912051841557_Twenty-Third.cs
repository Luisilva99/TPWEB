namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentyThird : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeHours", "TimeHourId", "dbo.StationPosts");
            DropIndex("dbo.TimeHours", new[] { "TimeHourId" });
            DropColumn("dbo.StationPosts", "TimeHourId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StationPosts", "TimeHourId", c => c.Int(nullable: false));
            CreateIndex("dbo.TimeHours", "TimeHourId");
            AddForeignKey("dbo.TimeHours", "TimeHourId", "dbo.StationPosts", "StationPostId");
        }
    }
}
