using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.Users;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IUserService
    {
        Task CreateUser();
        Task<User> GetUserById(int id);
    }
}