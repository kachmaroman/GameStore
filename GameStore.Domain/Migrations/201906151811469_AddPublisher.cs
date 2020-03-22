namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublisher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "PublisherId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "PublisherId");
            DropTable("dbo.Publishers");
        }
    }
}
