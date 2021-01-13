using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Banker.Tools;
using Banker.UIModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.APIRequestModel;
using Models.APIResponseModels;
using Newtonsoft.Json;

namespace Banker.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Config { get; }

        public HomeController(IConfiguration config)
        {
            Config = config;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UILogin model)
        {

            if (!ModelState.IsValid) return View(model);
            LoginResponse result=new LoginResponse();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(new Login { Email = model.Email, Password = model.Password }),
                    System.Text.Encoding.UTF8,
                    "application/json"
                    );
                using (var response = await httpClient.PostAsync(ServiceURL.GetURL(Config)+"Home/Login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                     result = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);
                }
            }

            if (!result.Success||result.User==null)
            {
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            var prop = new AuthenticationProperties()
            {
                IsPersistent = model.RebemberMe,
            };


            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier,result.User.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name,result.User.Email));
            claims.Add(new Claim("Token", result.Token));

            if (result.UserClaims != null)
                result.UserClaims.ForEach(c => claims.Add(new Claim(ClaimTypes.Role, c.Value)));


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principial = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principial, prop);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register model)
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
