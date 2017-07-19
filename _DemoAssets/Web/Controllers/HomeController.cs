using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            System.Text.StringBuilder envVars = new System.Text.StringBuilder();
            foreach (System.Collections.DictionaryEntry de in Environment.GetEnvironmentVariables())
                envVars.Append(string.Format("<strong>{0}</strong>:{1}<br \\>", de.Key, de.Value));

            ViewBag.EnvironmentVariables = envVars.ToString();

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}