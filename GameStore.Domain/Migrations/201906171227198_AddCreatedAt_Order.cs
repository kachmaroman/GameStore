namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAt_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreatedAt", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CreatedAt");
        }
    }
}
