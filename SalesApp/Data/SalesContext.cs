﻿using SalesApp.Interfaces;
using SalesApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Data
{
    class SalesContext : DbContext
    {
        public DbSet<Sale> Sales { get; set; }

        public DbSet<SalesPerson> People { get; set; }

        public DbSet<SalesRegion> Regions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public override int SaveChanges()
        {
            //ChangeTracker.DetectChanges();

            //// Get the state manager to changed entitties
            //var stateManager = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;

            //// "Soft" delete entities that implement IActive
            //var deletedEntities = stateManager
            //    .GetObjectStateEntries(EntityState.Detached)
            //    .Select(e => e.Entity)
            //    .OfType<IActive>()
            //    .ToArray();

            //foreach (var deletedEntity in deletedEntities)
            //{
            //    if (deletedEntity == null) continue;
            //    stateManager.ChangeObjectState(deletedEntity, EntityState.Modified);
            //    deletedEntity.Active = false;
            //}

            //// Auding for newly created entities

            //var createdEntities = stateManager
            //    .GetObjectStateEntries(EntityState.Added)
            //    .Select(e => e.Entity)
            //    .OfType<BaseModel>()
            //    .ToArray();

            //foreach (var createdEntity in createdEntities)
            //{
            //    createdEntity.CreatedDate = DateTime.Now;
            //    createdEntity.CreatedBy = Environment.UserName;
            //    createdEntity.UpdatedDate = DateTime.Now;
            //    createdEntity.UpdatedBy = Environment.UserName;
            //}

            //// Auditing for updated entities

            //var updatedEntities = stateManager
            //    .GetObjectStateEntries(EntityState.Modified)
            //    .Select(e => e.Entity)
            //    .OfType<BaseModel>()
            //    .ToArray();

            //foreach (var updatedEntity in updatedEntities)
            //{
            //    updatedEntity.UpdatedDate = DateTime.Now;
            //    updatedEntity.UpdatedBy = Environment.UserName;
            //}

            //return base.SaveChanges();



            ChangeTracker.DetectChanges();

            // Get the state mananger to changed entities.
            var stateManager = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager;

            // "Soft" delete entities that implement IActive

            var deletedEntities = stateManager
                .GetObjectStateEntries(EntityState.Deleted)
                .Select(e => e.Entity)
                .OfType<IActive>()
                .ToArray();

            foreach (var deletedEntity in deletedEntities)
            {
                if (deletedEntity == null) continue;
                stateManager.ChangeObjectState(deletedEntity, EntityState.Modified);
                deletedEntity.Active = false;
            }

            // Auditing for newly created entities

            var createdEntities = stateManager
                .GetObjectStateEntries(EntityState.Added)
                .Select(e => e.Entity)
                .OfType<BaseModel>()
                .ToArray();

            foreach (var createdEntity in createdEntities)
            {
                createdEntity.CreatedDate = DateTime.Now;
                createdEntity.CreatedBy = Environment.UserName;
                createdEntity.UpdatedDate = DateTime.Now;
                createdEntity.UpdatedBy = Environment.UserName;
            }

            // Auditing for updated entities

            var updatedEntities = stateManager
                .GetObjectStateEntries(EntityState.Modified)
                .Select(e => e.Entity)
                .OfType<BaseModel>()
                .ToArray();

            foreach (var updatedEntity in updatedEntities)
            {
                updatedEntity.UpdatedDate = DateTime.Now;
                updatedEntity.UpdatedBy = Environment.UserName;
            }

            // Save change to the database
            return base.SaveChanges();
        }
    }
}
