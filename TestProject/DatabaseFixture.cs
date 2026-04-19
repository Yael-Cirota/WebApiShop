using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class DatabaseFixture
    {
        public ShopContext Context { get; private set; }

        public DatabaseFixture()
        {
            // Set up the test database connection and initialize the context
            var dbName = $"ShopTests_{System.Guid.NewGuid():N}";
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Database={dbName};Trusted_Connection=True;")
                .Options;
            Context = new ShopContext(options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }
        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
