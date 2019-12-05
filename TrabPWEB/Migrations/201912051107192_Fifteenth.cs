namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fifteenth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeHours", "TimeHourId", "dbo.StationPosts");
            DropIndex("dbo.TimeHours", new[] { "TimeHourId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.TimeHours", "TimeHourId");
            AddForeignKey("dbo.TimeHours", "TimeHourId", "dbo.StationPosts", "StationPostId");
        }
    }
}
