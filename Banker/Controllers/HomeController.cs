using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Banker.Model;
using Banker.Repository;
using Banker.Tools;
using Banker.UIModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banker.Controllers
{
    public class HomeController : Controller
    {
        public IAppUserRepositoty Repositoty { get; }

        public HomeController(IAppUserRepositoty repositoty)
        {
            Repositoty = repositoty;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index");
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            
            if (!ModelState.IsValid) return View(model);
            var (u,b)=Repositoty.CheckUser(model.Email, model.Password);
            if (!b) {
                ModelState.AddModelError(string.Empty, "İstifadəçi adı və ya parol yanlışdır.");
                return View(model);
            } 

            var prop = new AuthenticationProperties()
            {
                IsPersistent = model.RebemberMe,
            };

            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,u.Id.ToString()),
                new Claim(ClaimTypes.Name,u.Email),                
            };
            var roles = Repositoty.GetUserRoles(u.Id);
            if (roles != null)
                roles.ForEach(c => claims.Add(new Claim(ClaimTypes.Role, c.Value)));

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
            if (ModelState.IsValid)
            {   
                var (id, b) = Repositoty.Insert(new AppUsers()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = new HashCreate().CreateHashString(model.Password)
                });
                if (id == -1) {
                    ModelState.AddModelError("", $"{model.Email} is alreay  use");
                    return View(model);
                }
                if (id == 0)
                {
                    ModelState.AddModelError("", $"Qeydiyatda xeta bas verdi");
                    return View(model);
                }
                return RedirectToAction("Login");
            }
            return View(model);
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
