using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Editions;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Events;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Locations;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Notifications;
using Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Microsoft.EgitimAPI.Infrastructure.Data
{

    //dotnet ef migrations add fırst --context egitimcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations

    public class EgitimContext : DbContext
    {
        public EgitimContext(DbContextOptions<EgitimContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<FavoriteCourse> FavoriteCourses { get; set; }

        public DbSet<Educator> Educators { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<GivenCourse> GivenCourses { get; set; }

        public DbSet<CourseContent> CourseContents { get; set; }
        
        public DbSet<TenantEducator> TenantEducator { get; set; }

        public DbSet<Comment> Comments { get; set; }
        
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }

        public DbSet<GivenEvent> GivenEvents { get; set; }

        public DbSet<AdvertisingCourse> AdvertisingCourses { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Edition> Editions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<GivenCourse>()
                .HasOne<Course>(sc => sc.Course)
                .WithMany(s => s.Owners)
                .HasForeignKey(sc => sc.CourseId);


            builder.Entity<GivenCourse>()
                .HasOne<Tenant>(sc => sc.Tenant)
                .WithMany(s => s.GivenCourses)
                .HasForeignKey(sc => sc.TenantId);

            #region Comment

            builder.Entity<Comment>()
                .HasOne(e => e.Educator)
                .WithMany(c => c.Comments)
                .HasForeignKey(ec => ec.EntityId);
            
            builder.Entity<Comment>()
                .HasOne(e => e.Tenant)
                .WithMany(c => c.Comments)
                .HasForeignKey(ec => ec.EntityId);
            
            builder.Entity<Comment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Comments)
                .HasForeignKey(ec => ec.EntityId);

            #endregion
            
        }
        
    }
}
