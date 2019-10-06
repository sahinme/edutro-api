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

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//        }
        
    }
}
