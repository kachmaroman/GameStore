using GameStore.Domain.Entities;
using System.Data.Entity;

namespace GameStore.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
	    public EfDbContext()
	    {
			Database.SetInitializer<EfDbContext>(null);
		}

        public DbSet<Game> Games { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Requirements> Requirements { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}