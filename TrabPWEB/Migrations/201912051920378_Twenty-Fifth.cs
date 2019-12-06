namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwentyFifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StationPosts", "TimeHourId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StationPosts", "TimeHourId");
        }
    }
}
