namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourtyFifth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StationPosts",
                c => new
                    {
                        StationPostId = c.Int(nullable: false, identity: true),
                        StationPostName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StationPostId);
            
            CreateTable(
                "dbo.TimeDatas",
                c => new
                    {
                        TimeDataId = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TimeDataId)
                .ForeignKey("dbo.StationPosts", t => t.TimeDataId)
                .Index(t => t.TimeDataId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeDatas", "TimeDataId", "dbo.StationPosts");
            DropIndex("dbo.TimeDatas", new[] { "TimeDataId" });
            DropTable("dbo.TimeDatas");
            DropTable("dbo.StationPosts");
        }
    }
}
