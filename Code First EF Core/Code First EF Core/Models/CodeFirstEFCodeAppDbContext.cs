using Microsoft.EntityFrameworkCore;

namespace Code_First_EF_Core.Models
{
    public class CodeFirstEFCodeAppDbContext : DbContext
    {
        public CodeFirstEFCodeAppDbContext()
        {
                
        }

        public CodeFirstEFCodeAppDbContext(DbContextOptions<CodeFirstEFCodeAppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Product> Products { get;set; }
        public DbSet<Order> Orders { get;set; }

    }
}
