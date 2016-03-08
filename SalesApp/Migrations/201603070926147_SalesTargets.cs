namespace SalesApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalesTargets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesPersons", "SaledTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 100.00M));
            AddColumn("dbo.SalesRegions", "SalesTarget", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 10000.00M));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalesRegions", "SalesTarget");
            DropColumn("dbo.SalesPersons", "SaledTarget");
        }
    }
}
