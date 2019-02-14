namespace Auction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CloseBidding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Close", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Close");
        }
    }
}
