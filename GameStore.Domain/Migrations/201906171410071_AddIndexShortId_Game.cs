namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexShortId_Game : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "ShortId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "ShortId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Games", new[] { "ShortId" });
            DropColumn("dbo.Games", "ShortId");
        }
    }
}
