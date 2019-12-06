namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirtySixth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeHours", "StationPost_StationPostId", "dbo.StationPosts");
            DropIndex("dbo.TimeHours", new[] { "StationPost_StationPostId" });
            DropColumn("dbo.TimeHours", "StationPost_StationPostId");
            DropTable("dbo.StationPosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StationPosts",
                c => new
                    {
                        StationPostId = c.Int(nullable: false, identity: true),
                        StationPostName = c.String(nullable: false),
                        TimeHourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StationPostId);
            
            AddColumn("dbo.TimeHours", "StationPost_StationPostId", c => c.Int());
            CreateIndex("dbo.TimeHours", "StationPost_StationPostId");
            AddForeignKey("dbo.TimeHours", "StationPost_StationPostId", "dbo.StationPosts", "StationPostId");
        }
    }
}
