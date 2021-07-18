namespace Vidly.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberInStock", c => c.Byte(nullable: false));
            DropColumn("dbo.Movies", "StockNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "StockNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "NumberInStock");
        }
    }
}
