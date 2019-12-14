namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnehundredFive : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StationAtributions",
                c => new
                    {
                        StationAtributionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        StationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StationAtributionId)
                .ForeignKey("dbo.Stations", t => t.StationId, cascadeDelete: true)
                .Index(t => t.StationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StationAtributions", "StationId", "dbo.Stations");
            DropIndex("dbo.StationAtributions", new[] { "StationId" });
            DropTable("dbo.StationAtributions");
        }
    }
}
