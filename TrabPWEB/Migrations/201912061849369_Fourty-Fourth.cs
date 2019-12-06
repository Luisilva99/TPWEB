namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourtyFourth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeDatas", "StationPost_StationPostId", "dbo.StationPosts");
            DropIndex("dbo.TimeDatas", new[] { "StationPost_StationPostId" });
            DropTable("dbo.StationPosts");
            DropTable("dbo.TimeDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TimeDatas",
                c => new
                    {
                        TimeDataId = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        StationPost_StationPostId = c.Int(),
                    })
                .PrimaryKey(t => t.TimeDataId);
            
            CreateTable(
                "dbo.StationPosts",
                c => new
                    {
                        StationPostId = c.Int(nullable: false, identity: true),
                        StationPostName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StationPostId);
            
            CreateIndex("dbo.TimeDatas", "StationPost_StationPostId");
            AddForeignKey("dbo.TimeDatas", "StationPost_StationPostId", "dbo.StationPosts", "StationPostId");
        }
    }
}
