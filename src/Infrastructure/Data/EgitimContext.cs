using Microsoft.EgitimAPI.ApplicationCore.Entities.Categories;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Courses;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Educators;
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
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TenantEducator>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });  
            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);  
            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);
        }
        
    }
}
