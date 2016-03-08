namespace SalesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesRegionChage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesPersons", "SalesTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SalesRegions", "Code", c => c.String(nullable: false, maxLength: 3));
            DropColumn("dbo.SalesPersons", "SaledTarget");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesPersons", "SaledTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.SalesRegions", "Code", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.SalesPersons", "SalesTarget");
        }
    }
}
