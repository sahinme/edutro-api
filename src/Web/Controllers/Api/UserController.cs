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
        private readonly IEmailSender _emailSender;

        public UserController(IUserService userService,
                IEmailSender emailSender
            )
        {
            _userService = userService;
            _emailSender = emailSender;
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

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto input)
        {
            await _userService.UpdateUser(input);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _userService.DeleteUser(id);
            return Ok();
        }

        [HttpPost]
        public async Task EmailSend(string email, string subject, string message)
        {
            await _emailSender.SendEmailAsync(email, subject, message);
        }
    }
}