using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Users> users { get; set; }
        public DbSet<Tasks> tasks { get; set; }
        public DbSet<TaskUsers> taskUsers { get; set; }
        public DbSet<TaskMessages> taskMessages { get; set; }

    }
}
