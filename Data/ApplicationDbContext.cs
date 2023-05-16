using Conference_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Conference_Management_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(e => e.FullName)
                .HasMaxLength(50);

            builder.Entity<ApplicationUser>()
                .Property(e => e.Expertises)
                .HasMaxLength(250);
        }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Paper> Papers { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}