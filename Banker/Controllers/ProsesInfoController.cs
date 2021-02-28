using Banker.Repository;
using Banker.UIModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Controllers
{
    public class ProsesInfoController : Controller
    {
        public ProsesInfoController()
        {
            rep = new Pos_Ins_UserRepository();
        }

        Pos_Ins_UserRepository rep { get; }

       

        public IActionResult AssingeInfo()
        {
            var roles=User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Role).Select(x => x.Value).ToList();
            var model = new UIProsInfo();

            var list = rep.GetPosInsUser(roles,getUserId());
            model.AssingePosision= list.Where(x=>x.UserId==0).ToList();
            model.AssingeMe = list.Where(x => x.UserId == getUserId()).ToList();
            return View(model);
        }

        public IActionResult Assinge(int id)
        {
            string[] clms = {"UserId" };
            object[] vals = { getUserId() };
            var b=rep.Update(clms, vals,id);
            var (p, bb) = rep.GetByColumNameFist("Id", id);
            return RedirectToAction(p.Step, p.PosesName,new {id=p.ProsessId});
        }


        public IActionResult ProsessReport()
        {
            return View();
        }

        public IActionResult SearchIns(UIReprotProsess model)
        {
            if (string.IsNullOrEmpty(model.ProsesName)) return View("ProsessReport",model);
            model.Response=rep.GetProsess(model);
            return View("ProsessReport", model);
        }
        [Authorize]
        public IActionResult MyProsess()
        {            
            var (list,b)=rep.GetByColumName("UserId", getUserId());
            if (!b) RedirectToAction("Index", "Home");
            var t = list;
            return View(list);
        }

        int getUserId()
        {
            int userId = 0;
            Int32.TryParse(User.Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value, out userId);
            return userId;
        }
    }
}
