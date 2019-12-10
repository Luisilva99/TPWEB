namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinetySixth : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TimeAtribuitions");
            AddColumn("dbo.TimeAtribuitions", "TimeAtribuitionId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TimeAtribuitions", new[] { "TimeAtribuitionId", "TimeDataId", "StationPostId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TimeAtribuitions");
            DropColumn("dbo.TimeAtribuitions", "TimeAtribuitionId");
            AddPrimaryKey("dbo.TimeAtribuitions", new[] { "TimeDataId", "StationPostId" });
        }
    }
}
