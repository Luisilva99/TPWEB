namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinetyFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeAtribuitions",
                c => new
                    {
                        TimeDataId = c.Int(nullable: false),
                        StationPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TimeDataId, t.StationPostId })
                .ForeignKey("dbo.StationPosts", t => t.StationPostId, cascadeDelete: true)
                .ForeignKey("dbo.TimeDatas", t => t.TimeDataId, cascadeDelete: true)
                .Index(t => t.TimeDataId)
                .Index(t => t.StationPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeAtribuitions", "TimeDataId", "dbo.TimeDatas");
            DropForeignKey("dbo.TimeAtribuitions", "StationPostId", "dbo.StationPosts");
            DropIndex("dbo.TimeAtribuitions", new[] { "StationPostId" });
            DropIndex("dbo.TimeAtribuitions", new[] { "TimeDataId" });
            DropTable("dbo.TimeAtribuitions");
        }
    }
}
