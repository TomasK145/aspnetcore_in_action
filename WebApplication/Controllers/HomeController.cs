using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue && (statusCode.Value == 404 || statusCode.Value == 500))
            {
                var viewName = statusCode.ToString();
                return View(viewName);
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TryDissablingFeatureForStatusCodePages()
        {
            //ASP.NET Core in Action - page 91
            var statusCodePagesFeature = HttpContext.Features.Get<IStatusCodePagesFeature>();
            if (statusCodePagesFeature != null)
            {
                statusCodePagesFeature.Enabled = false; //dissablovanim je mozne zneaktivnit funkcionalitu poskytovanu middlewarom, pr. ak nechcem v niektorych pripadoch umoznit zobrazenie stranky 404
            }

            return StatusCode(404);
        }
    }
}
