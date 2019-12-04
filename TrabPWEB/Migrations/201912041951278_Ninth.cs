namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ninth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StationPosts",
                c => new
                    {
                        StationPostId = c.Int(nullable: false, identity: true),
                        StationId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        Finnish = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StationPostId)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId);
            
            CreateTable(
                "dbo.TimeHours",
                c => new
                    {
                        TimeHourId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.TimeHourId)
                .ForeignKey("dbo.StationPosts", t => t.TimeHourId)
                .Index(t => t.TimeHourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeHours", "TimeHourId", "dbo.StationPosts");
            DropForeignKey("dbo.StationPosts", "StationId", "dbo.Stations");
            DropIndex("dbo.TimeHours", new[] { "TimeHourId" });
            DropIndex("dbo.StationPosts", new[] { "StationId" });
            DropTable("dbo.TimeHours");
            DropTable("dbo.StationPosts");
        }
    }
}
