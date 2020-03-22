namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShortId_Game : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "ShortId", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "ShortId");
        }
    }
}
