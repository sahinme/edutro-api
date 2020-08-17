using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EgitimAPI.ApplicationCore.Services.UserService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.EducatorService;
using Microsoft.EgitimAPI.ApplicationCore.Services.TenantService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class TokenController:BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ITenantAppService _tenantAppService;
        private readonly IEducatorAppService _educatorAppService;

        public TokenController(IConfiguration configuration,IUserService userService
        , ITenantAppService tenantAppService , IEducatorAppService educatorAppService)
        {
            _configuration = configuration;
            _userService = userService;
            _tenantAppService = tenantAppService;
            _educatorAppService = educatorAppService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("userToken")]
        public async Task<IActionResult>  Post([FromBody]LoginDto request)
        {
            if (ModelState.IsValid)
            {
                var userInfo = await _userService.Login(request); 
                if (userInfo==null)
                {
                    return Ok(new {success = false, status = 404, message = "Email veya sifre yanlis"});
                }
 
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, request.UsernameOrEmail),
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
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token),userInfo });
            }
            return BadRequest();
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("tenantOrEducatorToken")]
        public async Task<IActionResult> Post([FromBody] TenantOrEducatorLoginDto request)
        {
            if (ModelState.IsValid)
            {
                if (request.EntityType == "Tenant")
                {
                    var loginData = await _tenantAppService.Login(request); 
                    if (loginData==null)
                    {
                        return NotFound();
                    }
 
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, request.Email),
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
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token),loginData });
                }

                if (request.EntityType == "Educator")
                {
                    var loginData = await _educatorAppService.Login(request); 
                    if (loginData==null)
                    {
                        return NotFound();
                    }
 
                    
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, request.Email),
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
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token),loginData });
                }
            }
            return BadRequest();
        }
        
      
    }
}