using DutyFree.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace DutyFree.Web.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
<<<<<<< Updated upstream
        public DbSet<Order> Orders { get; set; }
=======
        public DbSet<Category> Categories { get; set; } 
>>>>>>> Stashed changes
    }
}
