namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePublisher : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "Publisher");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Publisher", c => c.String(nullable: false));
        }
    }
}
