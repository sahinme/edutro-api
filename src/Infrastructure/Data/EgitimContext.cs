using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
using Microsoft.EgitimAPI.ApplicationCore.Entities.TenantEducator;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Tenants;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.Infrastructure.Data
{

    //dotnet ef migrations add UserModel --context egitimcontext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations

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
            
            builder.Entity<GivenCourse>()
                .HasOne<Course>(sc => sc.Course)
                .WithMany(s => s.Owners)
                .HasForeignKey(sc => sc.CourseId);


            builder.Entity<GivenCourse>()
                .HasOne<Tenant>(sc => sc.Tenant)
                .WithMany(s => s.GivenCourses)
                .HasForeignKey(sc => sc.TenantId);
  
        }
        
    }
}
