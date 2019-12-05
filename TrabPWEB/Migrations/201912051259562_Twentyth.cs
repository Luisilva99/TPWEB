namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Twentyth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StationPosts", "StationPostName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StationPosts", "StationPostName");
        }
    }
}
