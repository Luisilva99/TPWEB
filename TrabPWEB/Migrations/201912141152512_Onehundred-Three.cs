namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnehundredThree : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StationPosts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Stations", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stations", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.StationPosts", "Price");
        }
    }
}
