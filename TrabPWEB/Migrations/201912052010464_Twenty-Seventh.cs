namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentySeventh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StationPosts", "TimeHourId_TimeHourId", "dbo.TimeHours");
            DropIndex("dbo.StationPosts", new[] { "TimeHourId_TimeHourId" });
            DropColumn("dbo.StationPosts", "TimeHourId_TimeHourId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StationPosts", "TimeHourId_TimeHourId", c => c.Int());
            CreateIndex("dbo.StationPosts", "TimeHourId_TimeHourId");
            AddForeignKey("dbo.StationPosts", "TimeHourId_TimeHourId", "dbo.TimeHours", "TimeHourId");
        }
    }
}
