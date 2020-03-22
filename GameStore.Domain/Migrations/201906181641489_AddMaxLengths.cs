namespace GameStore.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administrators", "Login", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Categories", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Publishers", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publishers", "Name", c => c.String());
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Administrators", "Login", c => c.String(nullable: false));
        }
    }
}
