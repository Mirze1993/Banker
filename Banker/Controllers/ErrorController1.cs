using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Banker.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error")]
        public IActionResult Error(int statusCode)
        {
            int x = 0;
            var aa = 4 / x;
            return View();
        }

        [Route("/Exception")]
        public IActionResult Exception(int statusCode)
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var l= LogManager.GetLogger("myLogger");
            l.Error($"\n {errorInfo.Path} \nmessage:{errorInfo.Error.Message} \nTrack: {errorInfo.Error.StackTrace} \n source:{errorInfo.Error.Source} \n innerException :{errorInfo.Error.InnerException}");
            return View("Error");
        }
    }
}
