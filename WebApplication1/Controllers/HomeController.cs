using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.APIRequestModel;
using Models.APIResponseModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {


        public HomeController(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }
        [Authorize()]
        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            var u = User.Claims;
            return Ok(new List<int>() { 3, 4, 5, 6, 7, 4, 3, 2 });

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        [ProducesResponseType(typeof(LoginResponse), 200)]        
        public IActionResult Login([FromBody] Login login)
        {
            var repository = new AppUsersRepository();
            var (u, b) = repository.CheckUser(login.Email, login.Password);
            if (u == null) return Ok(new LoginResponse { Success = false, Message = b });
            var roles = repository.GetUserRoles(u.Id);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),
                new Claim(ClaimTypes.Name,u.Email),
            };
            if (roles != null)
                roles.ForEach(c => claims.Add(new Claim(ClaimTypes.Role, c.Value)));


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Config["Token:Key"]));

            var token = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.Now.AddMinutes(10),
                claims: claims
               );
            var stringtoken = new JwtSecurityTokenHandler().WriteToken(token);


            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Success operation",
                User = u,
                UserClaims = roles,
                Token = stringtoken
            });

        }
    }
}
