using System.Linq;
using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;


namespace Microsoft.EgitimAPI.ApplicationCore.Services
{
    public class UserService:IUserService
    {
        private readonly IAsyncRepository<User> _asyncRepository;

        public UserService(IAsyncRepository<User> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }
        
        public async Task<User> CreateUser(CreateUserDto input)
        {
            var user = new User
            {
                Name = input.Name,
                Surname = input.Surname,
                EmailAddress = input.EmailAddress,
                Password = input.Password,
                Profession = input.Profession,
                Gender = input.Gender,
                Username = input.Username,
                Age = input.Age,
                PhoneNumber = input.PhoneNumber
            };
            await _asyncRepository.AddAsync(user);
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _asyncRepository.GetByIdAsync(id);
            return user;
        }

        public User GetUserForLogin(LoginDto input)
        {
            var user = _asyncRepository.GetAll()
                .FirstOrDefault(x => x.Username == input.Username && x.Password == input.Password);
            return user;
        }
    }
}