using Banker.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ProsessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Controllers
{
    
    public class ValuesController : Controller
    {
        public ValuesController()
        {
            repository = new AppUsersRepository();
        }

        public AppUsersRepository repository { get; private set; }

        [HttpPost]
        public JsonResult GetUser(int id)
        {
            var (u, b) = repository.GetByColumNameFist("Id",id,"Name");
            return Json(u);
        }

        
        public IActionResult GetUserInfo(int id)
        {
            var (u, b) = repository.GetByColumNameFist("Id", id);
            return PartialView("UserShowPartial",u);
        }

        public IActionResult GetBranchInfo(int id)
        {
            var (br, b) = repository.GetByColumNameFist<Branch>("Id", id);
            return PartialView("BranchShowPartial", br);
        }
    }
}
