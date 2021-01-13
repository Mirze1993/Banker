using Banker.ObjectsSelectListItem;
using Banker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Inistances;
using Models.ProsessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Controllers
{
    [Authorize]
    public class BiznesOverdraftController : Controller
    {
        [HttpPost]
        public IActionResult Step1()
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
            var inistance = new Ins_BiznesOverdraft
            {
                InitiatorName = User.Identity.Name,
                InitiatorId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value??"0"),
                ResponsibleName = User.Identity.Name,
                ResponsibleId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0"),
                StartDate = DateTime.Now,
                Status = ProcessStatus.Active,
            };
            var (id,b)=new Ins_BiznesOverdraftReposotory().Insert(inistance);
            if(b!) RedirectToAction("Index", "Home");
            var branchs = new List<Branch> {
                new Branch { Name="Baki",BranchCode="111",Id=1},
                new Branch { Name="Tovuz",BranchCode="100",Id=2},
                new Branch { Name="Qax",BranchCode="222",Id=3},
            };
            ViewBag.Branchs = CreateSelectListItems.GetSelectListItems(branchs);
            return View(inistance);
        }
        
    }
}
