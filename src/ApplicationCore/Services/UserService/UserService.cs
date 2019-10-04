using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.Users;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class UserService:IUserService
    {
        private readonly IAsyncRepository<User> _asyncRepository;

        public UserService(IAsyncRepository<User> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }
        
        public Task CreateUser()
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _asyncRepository.GetByIdAsync(id);
            return user;
        }
    }
}