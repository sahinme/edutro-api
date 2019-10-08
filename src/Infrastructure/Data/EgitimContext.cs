using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.Infrastructure.Data
{

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TenantEducator>().HasKey(te => new { te.TenantId, te.EducatorId });

            builder.Entity<TenantEducator>()
                .HasOne<Tenant>(sc => sc.Tenant)
                .WithMany(s => s.TenantEducators)
                .HasForeignKey(sc => sc.TenantId);


            builder.Entity<TenantEducator>()
                .HasOne<Educator>(sc => sc.Educator)
                .WithMany(s => s.EducatorTenants)
                .HasForeignKey(sc => sc.EducatorId);
            
            builder.Entity<GivenCourse>().HasKey(te => new { te.TenantId, te.CourseId });

            builder.Entity<GivenCourse>()
                .HasOne<Tenant>(sc => sc.Tenant)
                .WithMany(s => s.GivenCourses)
                .HasForeignKey(sc => sc.TenantId);


            builder.Entity<GivenCourse>()
                .HasOne<Course>(sc => sc.Course)
                .WithMany(s => s.Tenants)
                .HasForeignKey(sc => sc.CourseId);
            
            
//            builder.Entity<GivenCourse>().HasKey(te => new { te.EducatorId, te.CourseId });
//
//            builder.Entity<GivenCourse>()
//                .HasOne<Educator>(sc => sc.Educator)
//                .WithMany(s => s.GivenCourses)
//                .HasForeignKey(sc => sc.EducatorId);
//
//
//            builder.Entity<GivenCourse>()
//                .HasOne<Course>(sc => sc.Course)
//                .WithMany(s => s.Educators)
//                .HasForeignKey(sc => sc.CourseId);
        }
        
    }
}
