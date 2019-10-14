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
        private readonly IAsyncRepository<User> _userRepository;

        public UserService(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;
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
            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user;
        }

        public async Task UpdateUser(UpdateUserDto input)
        {
            var user = await _userRepository.GetByIdAsync(input.Id);
            user.Age = input.Age;
            user.Gender = input.Gender;
            user.Name = input.Name;
            user.Surname = input.Surname;
            user.Profession = input.Profession;
            user.EmailAddress = input.EmailAddress;
            user.Username = input.Username;
            user.PhoneNumber = input.PhoneNumber;
            
            await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> Login(LoginDto input)
        {
            var user = await _userRepository.GetAll()
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

        public async Task DeleteUser(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
        }
    }
}