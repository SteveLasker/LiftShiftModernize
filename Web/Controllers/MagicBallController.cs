using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_UI.Controllers
{
    public class MagicBallController : Controller
    {
        private string _doSomethingBaseAddress;
        private string _doSomethingAPIUrl;
        public MagicBallController()
        {
            _doSomethingBaseAddress = "http://api";
            #region SF Priviate Network Discovery Workaround
            // Workaround until network discovery is complete in SF
            // Would also be nice to have a way to know the container is running under SF to handle unique things like this
            // embedding the port here as we can't yet use environment variables in .NET FX/IIS apps
            if (Environment.GetEnvironmentVariable("USERNAME") != "ContainerAdministrator")
            {
                _doSomethingBaseAddress += ":" + (Environment.GetEnvironmentVariable("API_PORT") ?? "8001"); 
            }
            #endregion
            _doSomethingAPIUrl = "/Magic8BallApi";
            ViewBag.API_URL = _doSomethingBaseAddress + _doSomethingAPIUrl;
        }
        // GET: /<controller>/
        public ActionResult Index()
        {
            HttpResponseMessage response = null;
            //
            // Get the HttpRequest
            //
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Get, 
                        _doSomethingBaseAddress + _doSomethingAPIUrl);

                response = client.SendAsync(request).Result;
            }
            catch (Exception ex)
            {
                // eat the exception for now...
                ViewBag.Exception = ex.ToString();
            }

            //
            // Return a response from the Crazy 8 Ball Service
            //
            if (response != null && response.IsSuccessStatusCode)
            {
                List<Dictionary<String, String>> responseElements = new List<Dictionary<String, String>>();
                JsonSerializerSettings settings = new JsonSerializerSettings();
                String responseString = response.Content.ReadAsStringAsync().Result;
                ViewData["Answer"] = responseString;

            }
            return View();
        }
    }
}
