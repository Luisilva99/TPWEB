namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourtyThird : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeDatas", "StationPost_StationPostId", c => c.Int());
            CreateIndex("dbo.TimeDatas", "StationPost_StationPostId");
            AddForeignKey("dbo.TimeDatas", "StationPost_StationPostId", "dbo.StationPosts", "StationPostId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeDatas", "StationPost_StationPostId", "dbo.StationPosts");
            DropIndex("dbo.TimeDatas", new[] { "StationPost_StationPostId" });
            DropColumn("dbo.TimeDatas", "StationPost_StationPostId");
        }
    }
}
