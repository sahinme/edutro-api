using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Users;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class UserController:BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<User> GetById(int id)
        {
            return await _userService.GetUserById(id);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto input)
        {
           var user = await _userService.CreateUser(input);
           return Ok(user);
        }
    }
}