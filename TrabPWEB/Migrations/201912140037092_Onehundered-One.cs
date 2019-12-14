namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnehunderedOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MoneyAtribuitions",
                c => new
                    {
                        MoneyAtribuitionId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Cash = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MoneyAtribuitionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MoneyAtribuitions");
        }
    }
}
