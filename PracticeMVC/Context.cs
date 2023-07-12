using Microsoft.EntityFrameworkCore;
using PracticeMVC.Models;

namespace PracticeMVC
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>().HasIndex(p => p.Name).IsUnique();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<TVType> TVTypes { get; set; }
        public DbSet<TV> TVs { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<GoodDelivery> GoodsDelivery { get; set; }
    }
}
