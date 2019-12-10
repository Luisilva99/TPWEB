namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinetySeventh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeAtribuitions", "StationPostId", "dbo.StationPosts");
            DropForeignKey("dbo.TimeAtribuitions", "TimeDataId", "dbo.TimeDatas");
            DropIndex("dbo.TimeAtribuitions", new[] { "TimeDataId" });
            DropIndex("dbo.TimeAtribuitions", new[] { "StationPostId" });
            DropTable("dbo.TimeAtribuitions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TimeAtribuitions",
                c => new
                    {
                        TimeAtribuitionId = c.Int(nullable: false, identity: true),
                        TimeDataId = c.Int(nullable: false),
                        StationPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeAtribuitionId, t.TimeDataId, t.StationPostId });
            
            CreateIndex("dbo.TimeAtribuitions", "StationPostId");
            CreateIndex("dbo.TimeAtribuitions", "TimeDataId");
            AddForeignKey("dbo.TimeAtribuitions", "TimeDataId", "dbo.TimeDatas", "TimeDataId", cascadeDelete: true);
            AddForeignKey("dbo.TimeAtribuitions", "StationPostId", "dbo.StationPosts", "StationPostId", cascadeDelete: true);
        }
    }
}
