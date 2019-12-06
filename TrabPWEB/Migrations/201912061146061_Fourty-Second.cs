namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourtySecond : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.TimeHours", "StationPost_StationPostId", "dbo.StationPosts");
            //DropIndex("dbo.TimeHours", new[] { "StationPost_StationPostId" });
            //DropTable("dbo.TimeHours");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.TimeHours",
            //    c => new
            //        {
            //            TimeHourId = c.Int(nullable: false, identity: true),
            //            Time = c.DateTime(nullable: false),
            //            Status = c.Boolean(nullable: false),
            //            StationPost_StationPostId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.TimeHourId);
            
            //CreateIndex("dbo.TimeHours", "StationPost_StationPostId");
            //AddForeignKey("dbo.TimeHours", "StationPost_StationPostId", "dbo.StationPosts", "StationPostId");
        }
    }
}
