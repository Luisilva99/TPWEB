namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourtyEighth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StationPosts", "RechargeTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.StationPosts", "RechargeTypeId");
            AddForeignKey("dbo.StationPosts", "RechargeTypeId", "dbo.RechargeTypes", "RechargeTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StationPosts", "RechargeTypeId", "dbo.RechargeTypes");
            DropIndex("dbo.StationPosts", new[] { "RechargeTypeId" });
            DropColumn("dbo.StationPosts", "RechargeTypeId");
        }
    }
}
