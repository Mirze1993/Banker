using AspNetCore.Reporting;
using Banker.ObjectsSelectListItem;
using Banker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models.Inistances;
using Models.ProsessObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Controllers
{
    [Authorize]
    public class BiznesOverdraftController : Controller
    {
        public BiznesOverdraftController(IWebHostEnvironment environment)
        {
            Environment = environment;
            Repository = new Ins_BiznesOverdraftReposotory();
        }

        public IWebHostEnvironment Environment { get; }
        public Ins_BiznesOverdraftReposotory Repository { get; }

        [HttpPost]
        [Authorize(Roles = "Kad")]
        public IActionResult Step1()
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
            var inistance = new Ins_BiznesOverdraft
            {
                InitiatorName = User.Identity.Name,
                InitiatorId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0"),
                ResponsibleName = User.Identity.Name,
                ResponsibleId = Convert.ToInt32(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0"),
                StartDate = DateTime.Now,
                Status = ProcessStatus.Active,
            };
            var (id, b) = Repository.Insert(inistance);
            if (b!) RedirectToAction("Index", "Home");
            var (branchs, bbr) = Repository.GetAll<Branch>();

            var e = Enumerable.Range(0, 6).Select(x => DateTime.Now.AddMonths(x - 6).ToString("dd-MM-yyyy")).ToList();
            e.ForEach(x => inistance.Dovruyeler.Add(new Ins_BiznesOverdraft_DovrueList { Date = x, Ins_BiznesOverdraftId = id }));
            inistance.Id = id;
            ViewBag.Branchs = CreateSelectListItems.GetSelectListItems(branchs);
            return View(inistance);
        }

        [HttpPost]
        [Authorize(Roles = "Kad")]
        public IActionResult Step1End(int id, Ins_BiznesOverdraft model, List<Ins_BiznesOverdraft_DovrueList> dovrueLists)
        {
            string dovryeJson = "";
            if (dovrueLists?.Count > 0) dovryeJson = Newtonsoft.Json.JsonConvert.SerializeObject(dovrueLists);
            string[] names = { "Name", "Rate", "Voen", "Amount", "Aylar", "BranchId", "Cif", "DovruyyeNovu", "DovruyelerJson" };
            object[] valuse = { model.Name,model.Rate,model.Voen,model.Amount,model.Aylar,
                model.BranchId,model.Cif,model.DovruyyeNovu,dovryeJson };
            var b = Repository.Update(names, valuse, id);
            return Step2(id).Result;
        }


        public async Task<IActionResult> Step2(int id)
        {
            var model = Repository.GetByIdJoinUsers(id);
            #region reportcreate
            LocalReport report = new LocalReport(Path.Combine(Environment.WebRootPath, "report", "Report1.rdlc"));
            Dictionary<string, string> parametrs = new Dictionary<string, string>();
            parametrs.Add("Name", model.Name);
            parametrs.Add("Amount", model.Amount.ToString());
            var result = report.Execute(RenderType.Pdf, parameters: parametrs);
            #endregion
            #region fileCreate
            var filename = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".pdf";
            using (var fs = new FileStream(Path.Combine(Environment.WebRootPath, "documents", filename), FileMode.OpenOrCreate))
            {
                using var stream = new MemoryStream(result.MainStream);
                await stream.CopyToAsync(fs);
            }
            #endregion
            #region updateInstance            
            model.FileNames = string.IsNullOrEmpty(model.FileNames) ? filename : model.FileNames + "*" + filename;
            object[] names = { model.FileNames };
            string[] columns = { "FileNames" };
            var b = Repository.Update(columns, names, id);
            #endregion

            Repository.Insert(new Pos_Ins_User
            {
                PosesName = "BiznesOverdraft",
                Role = "HeadOfKad",
                ProsessId = model.Id,
                Step = "Step3",
            });

            return View("Step2", model);
        }


        [Authorize("HeadOfKad")]
        public IActionResult Step3(int id)
        {
            var model = Repository.GetByIdJoinUsers(id);
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Stop(int id)
        {
            string[] a = { "Status", "EndDate" };
            object[] o = { ProcessStatus.Deactive, DateTime.Now };
            var b = Repository.Update(a, o, id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");
            var fullname = Path.Combine(Environment.WebRootPath, "documents", filename);
            MemoryStream ms = new MemoryStream();
            using (var fs = new FileStream(fullname, FileMode.Open))
            {
                await fs.CopyToAsync(ms);
            }
            ms.Position = 0;
            return File(ms, "application/pdf", filename);
        }


    }
}
