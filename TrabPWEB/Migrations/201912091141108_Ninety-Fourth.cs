namespace TrabPWEB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NinetyFourth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StationPosts", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.StationPosts", "Finnish", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StationPosts", "Finnish");
            DropColumn("dbo.StationPosts", "Start");
        }
    }
}
