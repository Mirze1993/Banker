using Banker.Repository;
using Banker.UIModel;
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

        public IActionResult Info()
        {
            int userId = 0;
            Int32.TryParse(User.Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value, out userId);

            var roles=User.Claims.Where(x => x.Type == System.Security.Claims.ClaimTypes.Role).Select(x => x.Value).ToList();
            var model = new UIProsInfo();

            model.AssingePosision= rep.getProsess(roles);
            model.AssingeMe = rep.GetByColumName("UserID", userId).Item1;
            return View(model);
        }

        public IActionResult Assinge(int id)
        {
            int userId = 0;
            Int32.TryParse(User.Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value, out userId);

            string[] clms = { "Role", "UserId" };
            object[] vals = { "", userId };
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
            return View("ProsessReport");
        }
    }
}
