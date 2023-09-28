using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCSimpleCRM.Models;

namespace MVCSimpleCRM.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Users> users { get; set; }
        public DbSet<Tasks> tasks { get; set; }
        public DbSet<TaskUsers> taskUsers { get; set; }
        public DbSet<TaskMessages> taskMessages { get; set; }
        public DbSet<AspNetUsers> aspNetUsers { get; set; }

    }
}
