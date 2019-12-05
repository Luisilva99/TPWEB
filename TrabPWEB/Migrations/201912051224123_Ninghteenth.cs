namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ninghteenth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StationPosts", "StationId", "dbo.Stations");
            DropIndex("dbo.StationPosts", new[] { "StationId" });
            DropColumn("dbo.StationPosts", "StationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StationPosts", "StationId", c => c.Int(nullable: false));
            CreateIndex("dbo.StationPosts", "StationId");
            AddForeignKey("dbo.StationPosts", "StationId", "dbo.Stations", "StationId", cascadeDelete: true);
        }
    }
}
