using ClipzAPI.Configuration;
using ClipzAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClipzAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AspNetUsers>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Ratings> Ratings { get; set; }
        public DbSet<Services> Services { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AspNetUsers>().HasMany(r => r.Ratings );
            modelBuilder.Entity<AspNetUsers>().HasMany(s => s.Services );
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
        }

    }
}
