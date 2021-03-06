using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;

namespace Microsoft.EgitimAPI.ApplicationCore.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserDto input);
        Task<User> GetUserById(int id);
        Task UpdateUser(UpdateUserDto input);
        Task<bool> Login(LoginDto input);
        Task DeleteUser(long id);
    }
}