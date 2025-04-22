using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Backend_SafeSpace
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        public DbSet<Profile> profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<User>()
                .HasOne(u => u.profile)
                 .WithOne(p => p.user)
                .HasForeignKey<Profile>(p => p.UserId);
        }
    }

}
