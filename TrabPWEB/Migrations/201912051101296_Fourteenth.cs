namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fourteenth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stations", "Start", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Stations", "Finnish", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.TimeHours", "Time", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeHours", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Stations", "Finnish", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Stations", "Start", c => c.DateTime(nullable: false));
        }
    }
}
