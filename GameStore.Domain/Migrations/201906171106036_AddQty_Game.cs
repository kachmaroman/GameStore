namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQty_Game : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Quantity", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Quantity");
        }
    }
}
