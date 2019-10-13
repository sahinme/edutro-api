using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.PasswordHasher;
using Microsoft.EntityFrameworkCore;


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
                Profession = input.Profession,
                Gender = input.Gender,
                Username = input.Username,
                Age = input.Age,
                PhoneNumber = input.PhoneNumber
            };
            var hashedPassword = SecurePasswordHasherHelper.Hash(input.Password);
            user.Password = hashedPassword;
            await _asyncRepository.AddAsync(user);
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _asyncRepository.GetByIdAsync(id);
            return user;
        }

        public async Task<bool> Login(LoginDto input)
        {
            var user = await _asyncRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Username == input.Username);
            if (user == null)
            {
                throw new Exception("There is no user!");
            }
            var decodedPassword = SecurePasswordHasherHelper.Verify(input.Password, user.Password);
            if (!decodedPassword)
            {
                return false;
            }

            return true;
        }
    }
}