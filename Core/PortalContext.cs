using Core.Models;
using Core.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class PortalContext : DbContext
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public PortalContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
            modelBuilder
                .Entity<ApplicationUser>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

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

        public override int SaveChanges()
        {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IAuditableEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries)
            {
                // If the entity state is Added let's set
                // the CreatedAt and CreatedBy properties
                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditableEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
                    ((IAuditableEntity)entityEntry.Entity).CreatedBy = _httpContextAccessor?.HttpContext?.User?.Claims?
                        .FirstOrDefault(claims => claims.Type == ClaimTypes.Actor).Value ?? "MyApp";
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                    Entry((IAuditableEntity)entityEntry.Entity).Property(p => p.CreatedDate).IsModified = false;
                    Entry((IAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                // In any case we always want to set the properties
                // ModifiedAt and ModifiedBy
                ((IAuditableEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;
                ((IAuditableEntity)entityEntry.Entity).UpdatedBy = _httpContextAccessor?.HttpContext?.User?.Claims?
                    .FirstOrDefault(claims => claims.Type == ClaimTypes.Actor).Value ?? "MyApp";
            }

            // After we set all the needed properties
            // we call the base implementation of SaveChangesAsync
            // to actually save our entities in the database
            return base.SaveChanges();
        }
    }
}
