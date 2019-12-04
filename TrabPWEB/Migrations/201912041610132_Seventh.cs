namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seventh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stations", "RegionId", "dbo.Regions");
            DropIndex("dbo.Stations", new[] { "RegionId" });
            CreateTable(
                "dbo.Locals",
                c => new
                    {
                        LocalId = c.Int(nullable: false, identity: true),
                        LocalName = c.String(nullable: false, maxLength: 150),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocalId)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            AddColumn("dbo.Stations", "LocalId", c => c.Int(nullable: false));
            CreateIndex("dbo.Stations", "LocalId");
            AddForeignKey("dbo.Stations", "LocalId", "dbo.Locals", "LocalId", cascadeDelete: true);
            DropColumn("dbo.Stations", "RegionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stations", "RegionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Stations", "LocalId", "dbo.Locals");
            DropForeignKey("dbo.Locals", "RegionId", "dbo.Regions");
            DropIndex("dbo.Stations", new[] { "LocalId" });
            DropIndex("dbo.Locals", new[] { "RegionId" });
            DropColumn("dbo.Stations", "LocalId");
            DropTable("dbo.Locals");
            CreateIndex("dbo.Stations", "RegionId");
            AddForeignKey("dbo.Stations", "RegionId", "dbo.Regions", "RegionId", cascadeDelete: true);
        }
    }
}
