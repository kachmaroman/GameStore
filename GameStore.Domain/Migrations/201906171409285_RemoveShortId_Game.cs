namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveShortId_Game : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "ShortId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "ShortId", c => c.Int(nullable: false));
        }
    }
}
