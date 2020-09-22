using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class PortalContext : DbContext
    {
        public PortalContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<UsersSubjects> UsersSubjectsRelation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(c => c.Reviews)
        //        .WithOne(e => e.ReviewingUser)
        //        .

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(c => c.Reviews)

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(c => c.Appointments)
        //        .WithOne(e => e.Teacher);

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(c => c.Appointments)
        //        .WithOne(e => e.Student);

        //    modelBuilder.Entity<ApplicationUser>()
        //        .HasMany(c => c.Posts)
        //        .WithOne(e => e.User);
        }
    }
}
