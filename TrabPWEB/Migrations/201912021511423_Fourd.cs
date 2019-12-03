namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        StationId = c.Int(nullable: false, identity: true),
                        StationName = c.String(nullable: false, maxLength: 50),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StationId)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            DropIndex("dbo.Stations", new[] { "RegionId" });
            DropTable("dbo.Stations");
        }
    }
}
