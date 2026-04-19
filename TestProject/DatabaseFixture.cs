using Microsoft.EntityFrameworkCore;
using Repositories;
using System;

namespace TestProject
{
    public class DatabaseFixture : IDisposable
    {
        public ShopContext Context { get; private set; }

        public DatabaseFixture()
        {
            // Use EF InMemory database for tests to avoid LocalDB conflicts
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(databaseName: "ShopTests_" + Guid.NewGuid().ToString())
                .Options;

            Context = new ShopContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
