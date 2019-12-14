namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnehundredSix : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Reserves");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reserves",
                c => new
                    {
                        ReserveId = c.Int(nullable: false, identity: true),
                        StationPostId = c.Int(nullable: false),
                        LocalId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        date = c.DateTime(nullable: false),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReserveId);
            
        }
    }
}
