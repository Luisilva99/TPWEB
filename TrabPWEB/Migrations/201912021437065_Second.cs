namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        RegionName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RegionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Regions");
        }
    }
}
