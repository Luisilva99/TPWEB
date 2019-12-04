namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Eleventh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stations", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Stations", "Finnish", c => c.DateTime(nullable: false));
            DropColumn("dbo.StationPosts", "Start");
            DropColumn("dbo.StationPosts", "Finnish");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StationPosts", "Finnish", c => c.DateTime(nullable: false));
            AddColumn("dbo.StationPosts", "Start", c => c.DateTime(nullable: false));
            DropColumn("dbo.Stations", "Finnish");
            DropColumn("dbo.Stations", "Start");
        }
    }
}
