namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentySixth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StationPosts", "TimeHourId_TimeHourId", c => c.Int());
            CreateIndex("dbo.StationPosts", "TimeHourId_TimeHourId");
            AddForeignKey("dbo.StationPosts", "TimeHourId_TimeHourId", "dbo.TimeHours", "TimeHourId");
            DropColumn("dbo.StationPosts", "TimeHourId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StationPosts", "TimeHourId", c => c.Int(nullable: false));
            DropForeignKey("dbo.StationPosts", "TimeHourId_TimeHourId", "dbo.TimeHours");
            DropIndex("dbo.StationPosts", new[] { "TimeHourId_TimeHourId" });
            DropColumn("dbo.StationPosts", "TimeHourId_TimeHourId");
        }
    }
}
