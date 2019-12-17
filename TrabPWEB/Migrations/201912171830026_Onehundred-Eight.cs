namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnehundredEight : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserves", "RechargeModId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reserves", "RechargeModId");
            AddForeignKey("dbo.Reserves", "RechargeModId", "dbo.RechargeMods", "RechargeModId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reserves", "RechargeModId", "dbo.RechargeMods");
            DropIndex("dbo.Reserves", new[] { "RechargeModId" });
            DropColumn("dbo.Reserves", "RechargeModId");
        }
    }
}
