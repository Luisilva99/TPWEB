namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourtySeventh : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeDatas", "TimeDataId", "dbo.StationPosts");
            DropIndex("dbo.TimeDatas", new[] { "TimeDataId" });
            CreateTable(
                "dbo.RechargeMods",
                c => new
                    {
                        RechargeModId = c.Int(nullable: false, identity: true),
                        RechargeModName = c.String(nullable: false, maxLength: 30),
                        RechargeModDescription = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.RechargeModId);
            
            CreateTable(
                "dbo.RechargeTypes",
                c => new
                    {
                        RechargeTypeId = c.Int(nullable: false, identity: true),
                        RechargeTypeName = c.String(nullable: false, maxLength: 30),
                        RechargeTypeDescription = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.RechargeTypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RechargeTypes");
            DropTable("dbo.RechargeMods");
            CreateIndex("dbo.TimeDatas", "TimeDataId");
            AddForeignKey("dbo.TimeDatas", "TimeDataId", "dbo.StationPosts", "StationPostId");
        }
    }
}
