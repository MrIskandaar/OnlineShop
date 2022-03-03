using Root.Tables;

namespace Root
{
    public partial class ShopContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
