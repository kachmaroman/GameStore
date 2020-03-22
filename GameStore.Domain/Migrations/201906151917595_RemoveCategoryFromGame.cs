namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCategoryFromGame : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Category", c => c.String());
        }
    }
}
