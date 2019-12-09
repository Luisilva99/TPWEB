namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinetyFifth : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TimeAtribuitions", new[] { "TimeDataId" });
            DropIndex("dbo.TimeAtribuitions", new[] { "StationPostId" });
            DropPrimaryKey("dbo.TimeAtribuitions");
            AlterColumn("dbo.TimeAtribuitions", "TimeDataId", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeAtribuitions", "StationPostId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.TimeAtribuitions", new[] { "TimeDataId", "StationPostId" });
            CreateIndex("dbo.TimeAtribuitions", "TimeDataId");
            CreateIndex("dbo.TimeAtribuitions", "StationPostId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TimeAtribuitions", new[] { "StationPostId" });
            DropIndex("dbo.TimeAtribuitions", new[] { "TimeDataId" });
            DropPrimaryKey("dbo.TimeAtribuitions");
            AlterColumn("dbo.TimeAtribuitions", "StationPostId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.TimeAtribuitions", "TimeDataId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TimeAtribuitions", new[] { "TimeDataId", "StationPostId" });
            CreateIndex("dbo.TimeAtribuitions", "StationPostId");
            CreateIndex("dbo.TimeAtribuitions", "TimeDataId");
        }
    }
}
