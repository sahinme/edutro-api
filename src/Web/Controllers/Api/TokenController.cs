using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class TokenController:BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public TokenController(IConfiguration configuration,IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult>  Post([FromBody]LoginDto request)
        {
            if (ModelState.IsValid)
            {
                var isUserValid = await _userService.Login(request); 
                if (!isUserValid)
                {
                    throw new Exception("Username or password invalid !");
                }
 
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
 
                var token = new JwtSecurityToken
                (
                    issuer: _configuration["Issuer"], 
                    audience: _configuration["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(30), // 30 gün geçerli olacak
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),//appsettings.json içerisinde bulunan signingkey değeri
                        SecurityAlgorithms.HmacSha256)
                );
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return BadRequest();
        }
    }
}