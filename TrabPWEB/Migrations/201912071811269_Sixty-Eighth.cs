namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SixtyEighth : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TimeAtribuitions",
                c => new
                    {
                        TimeDataId = c.Int(nullable: false, identity: true),
                        StationPostId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => new { t.TimeDataId, t.StationPostId });
            
            CreateIndex("dbo.TimeAtribuitions", "StationPostId", clustered: true);
            CreateIndex("dbo.TimeAtribuitions", "TimeDataId", clustered: true);
            AddForeignKey("dbo.TimeAtribuitions", "TimeDataId", "dbo.TimeDatas", "TimeDataId", cascadeDelete: true);
            AddForeignKey("dbo.TimeAtribuitions", "StationPostId", "dbo.StationPosts", "StationPostId", cascadeDelete: true);
        }
    }
}
