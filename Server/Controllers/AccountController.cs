using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using IConfiguration =  Microsoft.Extensions.Configuration.IConfiguration;

namespace BlazorMovies.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<IdentityUser> _userManager,
                                 SignInManager<IdentityUser> _signinManager,
                                 IConfiguration _config
                                )
        {
            this._userManager = _userManager;
            this._signinManager = _signinManager;
            this._config = _config;
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserToken>> CreateUser ([FromBody] UserInfo userInfo)
        {
            var user = new IdentityUser { UserName = userInfo.Email, Email = userInfo.Email,  };
            var result = await _userManager.CreateAsync(user, userInfo.Password);
            if (result.Succeeded)
            {
                return Ok(await CreateToken(userInfo));
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login ([FromBody] UserInfo info)
        {
            var login = await _signinManager.PasswordSignInAsync(info.Email, info.Password, true, false);
            if (login.Succeeded)
            {
                return Ok(await CreateToken(info));
            }
            return BadRequest();
        }
        private async Task<UserToken> CreateToken (UserInfo userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Name, userInfo.Email)
            };

            var user = await _userManager.FindByNameAsync(userInfo.Email);
            claims.AddRange(await _userManager.GetClaimsAsync(user));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddYears(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiry,
                signingCredentials: creds
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = expiry
            };
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
