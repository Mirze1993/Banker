using Banker.Tools;
using Banker.UIModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.APIResponseModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Banker.Controllers
{
    public class UserInfoController : Controller
    {
        public UserInfoController(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }

        public async Task<IActionResult> Profile()
        {
            var token = User.Claims.First(x => x.Type == "Token").Value;
            int id=0;
            Int32.TryParse(User.Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value,out id);
            AppUserResponse result = new AppUserResponse();
            if (id == 0) RedirectToAction("Index", "Home");
            using (HttpClient client =new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                using (var response=await client.GetAsync(ServiceURL.GetURL(Config) + "Home/GetUserById/"+id.ToString()))
                {
                    var content=await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<AppUserResponse>(content);
                }
            }
            if (result == null) return View();
            var model = new UIAppUser {
                Email = result.User.Email,
                Id = result.User.Id,
                Name = result.User.Name,
                userClaims = result.UserClaims
            };
            return View(model);
        }
    }
}
