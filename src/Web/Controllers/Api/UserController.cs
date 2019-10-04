using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.Users;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class UserController:BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        public async Task<User> List(int id)
        {
            return await _userService.GetUserById(id);
        }
    }
}