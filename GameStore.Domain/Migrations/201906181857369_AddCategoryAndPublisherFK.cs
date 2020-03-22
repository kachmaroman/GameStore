namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryAndPublisherFK : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Categories", "Name", c => c.String(maxLength: 30));
        }
    }
}
