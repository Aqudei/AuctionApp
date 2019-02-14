namespace Auction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RePassword1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "UserName", c => c.String(maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "UserName", c => c.String());
        }
    }
}
