namespace SalesApp.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesApp.Data.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SalesApp.Data.SalesContext context)
        {      
             // Seed Sales Regions
              context.Regions.AddOrUpdate(
                p => p.Id,
                new SalesRegion {
                    Active = true,
                    Code = "NTH",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    Id = 1,
                    Name = "North Region",
                    SalesTarget = 10000.00M,
                    UpdatedBy = "Admin",
                    UpdatedDate = DateTime.Now
                },
                new SalesRegion
                {
                    Active = true,
                    Code = "STH",
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    Id = 2,
                    Name = "South Region",
                    SalesTarget = 10000.00M,
                    UpdatedBy = "Admin",
                    UpdatedDate = DateTime.Now
                }                
            );

            // Seed Sales People
            context.People.AddOrUpdate(
                p => p.Id,
                new SalesPerson
                {
                    Active = true,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    FirstName = "Bob",
                    Id = 1,
                    LastName = "Smith",
                    RegionId = 1,
                    SalesTarget = 2000.00M,
                    UpdatedBy = "Admin",
                    UpdatedDate = DateTime.Now
                }
            );

            // Seed Test Sale
            context.Sales.AddOrUpdate(
                p => p.Id,
                new Sale
                {
                    Amount = 321.45M,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now,
                    Date = new DateTime(2015, 3, 3),
                    Id = 1,
                    PersonId = 1,
                    RegionId = 1,
                    UpdatedBy = "Admin",
                    UpdatedDate = DateTime.Now
                }
            );
        }
    }
}
