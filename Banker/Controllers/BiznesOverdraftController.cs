using AspNetCore.Reporting;
using Banker.Tools;
using Banker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Models.Inistances;
using Models.ProsessObjects;
using Newtonsoft.Json;
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
        public IActionResult Start()
        {
            #region CreateInstance
            var inistance = new Ins_BiznesOverdraft
            {                
                InitiatorId = getUserId(),
                ResponsibleId = getUserId(),
                StartDate = DateTime.Now,
                Status = ProcessStatus.Active,
            };
            #region addMounrs inistance.DovruyeList
            var e = Enumerable.Range(0, 6).Select(x => DateTime.Now.AddMonths(x - 6).ToString("dd-MM-yyyy")).ToList();
            e.ForEach(x => inistance.Dovruyeler.Add(new Ins_BiznesOverdraft_DovrueList { Date = x }));
            inistance.DovruyelerJson = JsonConvert.SerializeObject(inistance.Dovruyeler);
            #endregion
            var (id, b) = Repository.Insert(inistance);
            if (b!) RedirectToAction("Index", "Home");
            #endregion

            #region assingcreate            
            Repository.Insert(new Pos_Ins_User
            {
                PosesName = "BiznesOverdraft",
                ProsessId = id,
                Step = "Step1",
                UserId = getUserId()
            });
            #endregion
            return Step1(id);
        }

        [Authorize(Roles = "Kad")]
        public IActionResult Step1(int id)
        {
            var model = Repository.GetById(id);

            return View("Step1", model);
        }

        [HttpPost]
        [Authorize(Roles = "Kad")]
        public async Task<IActionResult> Step1End(int id, Ins_BiznesOverdraft model, List<Ins_BiznesOverdraft_DovrueList> dovrueLists)
        {
            #region reportcreate
            LocalReport report = new LocalReport(Path.Combine(Environment.WebRootPath, "report", "Report1.rdlc"));
            Dictionary<string, string> parametrs = new Dictionary<string, string>();
            parametrs.Add("Name", model.Name);
            parametrs.Add("Amount", model.Amount.ToString());
            var result = report.Execute(RenderType.Pdf, parameters: parametrs);
            #endregion
            #region fileCreate
            var filename = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-Muqavile") + ".pdf";
            using (var fs = new FileStream(Path.Combine(Environment.WebRootPath, "documents", filename), FileMode.OpenOrCreate))
            {
                using var stream = new MemoryStream(result.MainStream);
                await stream.CopyToAsync(fs);
            }
            #endregion
            #region UpdateIns
            string dovryeJson =  JsonConvert.SerializeObject(dovrueLists);
            string[] names = { "Name", "Rate", "Voen", "Amount",
                "Aylar", "BranchId", "Cif", "DovruyyeNovu", "DovruyelerJson", "FileNames" };
            object[] valuse = { model.Name,model.Rate,model.Voen,model.Amount,model.Aylar,
                model.BranchId,model.Cif,model.DovruyyeNovu,dovryeJson,filename };
            var b = Repository.Update(names, valuse, id);
            #endregion

            #region UpdateAssing
            Repository.Update<Pos_Ins_User>(
                new string[] { "Step", "InsName" }
                , new object[] { "Step2", $"BiznesOverdarft/{model.Name}/{model.Cif}" }
                , Repository.GetPosInsId(id));
            #endregion
            return Step2(id);
        }

        [Authorize(Roles = "Kad")]
        public IActionResult Step2(int id)
        {
            var model = Repository.GetById(id);
            return View("Step2", model);
        }

        [HttpPost]
        [Authorize(Roles = "Kad")]
        public IActionResult Step2End(int id)
        {
            #region UpdateAssing           
            Repository.Update<Pos_Ins_User>(new string[] { "Step", "Role", "UserId" }, new object[] { "Step3", "HeadOfKad", 0 }, Repository.GetPosInsId(id));
            #endregion

            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "HeadOfKad")]
        public IActionResult Step3(int id)
        {            
            var model = Repository.GetById(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "HeadOfKad")]
        public IActionResult Step3End(int id)
        {
            #region update Inst
            var b = Repository.Update(new string[] { "ResponsibleId" }, new object[] { getUserId() }, id);
            #endregion
            return Stop(id);
        }

        [Authorize]
        public IActionResult Info(int id)
        {
            var model = Repository.GetById(id);
            return View(model);
        }


        [HttpPost]
        public IActionResult Stop(int id)
        {
            string[] names = { "Status", "EndDate" };
            object[] values = { ProcessStatus.Deactive, DateTime.Now };
            var b = Repository.Update(names, values, id);
            Repository.Delet<Pos_Ins_User>(Repository.GetPosInsId(id));
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

        int getUserId()
        {
            int userId = 0;
            Int32.TryParse(User.Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value, out userId);
            return userId;
        }
    }
}
