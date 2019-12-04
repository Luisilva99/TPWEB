namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tenth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeHours", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.TimeHours", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeHours", "Status");
            DropColumn("dbo.TimeHours", "Time");
        }
    }
}
