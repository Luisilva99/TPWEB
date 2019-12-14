namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnehundredSeven : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reserves",
                c => new
                    {
                        ReserveId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        StationPostId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Completed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReserveId)
                .ForeignKey("dbo.StationPosts", t => t.StationPostId, cascadeDelete: true)
                .Index(t => t.StationPostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reserves", "StationPostId", "dbo.StationPosts");
            DropIndex("dbo.Reserves", new[] { "StationPostId" });
            DropTable("dbo.Reserves");
        }
    }
}
