global using Microsoft.EntityFrameworkCore;

namespace Root
{
    public partial class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options) : base(options)
        {
        }

        public ShopContext()
        {
        }

        public static string ConnectionString { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString,
                builder => { builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(2), null); });
        }
    }
}