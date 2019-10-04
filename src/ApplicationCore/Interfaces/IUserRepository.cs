using Microsoft.eShopWeb.ApplicationCore.Entities.Users;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IUserRepository:IAsyncRepository<User>
    {
        
    }
}