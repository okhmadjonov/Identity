using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        private const string CONNECTION_STRING = "Host=localhost;Port=5432;" +
              "Username=postgres;" +
              "Password=root;" +
              "Database=iaa";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(CONNECTION_STRING);
        }


        DbSet<User> Users { get; set; }





    }
}
