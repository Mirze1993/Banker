using AspNetCore.Reporting;
using Banker.Model;
using Banker.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banker.Controllers
{
    [Authorize]
    public class KreditController : Controller
    {
        KreditRepository repository;
        private readonly IWebHostEnvironment Environment;

        public KreditController(IWebHostEnvironment environment)
        {
            repository = new KreditRepository();
            Environment = environment;
        }

       

        [HttpPost]
        public IActionResult Start()
        {
            if (ModelState.IsValid)
            {
                var model = new Model.Kredit()
                {
                    Initiator = User.Identity.Name,
                    StartDate = DateTime.Now,
                    Status = ProcessStatus.Active
                };
                repository.Insert(model) ;
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public IActionResult Step1(Kredit model)
        {

            LocalReport report = new LocalReport(System.IO.Path.Combine(Environment.WebRootPath, "report", "Report1.rdlc"));
            Dictionary<string, string> parametrs = new Dictionary<string, string>();
            parametrs.Add("Name", model.Name);
            parametrs.Add("Amount", model.Amount.ToString());
            var result=report.Execute(RenderType.Word, parameters: parametrs);
            return File(result.MainStream, "text/plain", "File Result.doc");
        }


        
    }
}
