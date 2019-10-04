using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.Users;

namespace Microsoft.eShopWeb.Infrastructure.Data
{

    public class EgitimContext : DbContext
    {
        public EgitimContext(DbContextOptions<EgitimContext> options) : base(options)
        {
        }
        
        
        public DbSet<User> Users { get; set; }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//        }
        
    }
}
