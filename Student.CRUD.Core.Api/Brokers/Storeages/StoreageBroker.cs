﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Student.CRUD.Brokers.Storeages;

namespace StudentCRUD.Core.Api.Brokers.Storages
{
    public partial class StorageBroker : ExceptionContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public IStorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        private async ValueTask<T> InsertAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration);

            broker.Entry(@object).State = EntityState.Added;
            await broker.SaveChangesAsync();

            return @object;
        }

        public IQueryable<T> SelectAll<T>() where T : class
        {
            var broker = new StorageBroker(this.configuration);
            return broker.Set<T>();
        }

        private async ValueTask<T> SelectAsync<T>(params object[] objectIds) where T : class =>
            await FindAsync<T>(objectIds);

        public async ValueTask<T> UpdateAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Modified;
            await broker.SaveChangesAsync();

            return @object;
        }

        public async ValueTask<T> DeleteAsync<T>(T @object)
        {
            var broker = new StorageBroker(this.configuration);
            broker.Entry(@object).State = EntityState.Deleted;
            await broker.SaveChangesAsync();

            return @object;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString =
            this.configuration.GetConnectionString("ApplicationConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}