using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Statsig;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static bool IsInitialized = false;
        public async Task<JsonResult> Index()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;
                await Statsig.Server.StatsigServer.Initialize("secret-yyyyy");
            }
            
            var user = new StatsigUser
            {
                UserID = "b-user"
            };
            
            var gate = Statsig.Server.StatsigServer.GetFeatureGate(user, "a_gate");
            
            var data = new
            {
                Message = "This is the Foo page.",
                Gate = gate.Name,
                GateValue = gate.Value,
                Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}