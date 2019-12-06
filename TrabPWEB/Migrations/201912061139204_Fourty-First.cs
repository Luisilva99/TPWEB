namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FourtyFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeDatas",
                c => new
                    {
                        TimeDataId = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TimeDataId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TimeDatas");
        }
    }
}
