using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPIRest.Controllers
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
            return Ok(new List<int>() {3,4,5,6,7,4,3,2 });
            
        }
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Config["Token:Key"]));

            var token = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256),
                expires: DateTime.Now.AddMinutes(2),
                claims: new List<Claim>{ new Claim(ClaimTypes.Name,"Mirze")}
               );
            var stringtoken=new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(stringtoken);
        }

    }
}
