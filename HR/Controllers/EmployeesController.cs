using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Formatting;

namespace HR.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employee
        string url = "http://localhost:46466";
        //public async Task<ActionResult> Index()
        //{
        //    List<Employees> EmInfo = new List<Employees>();
        //    using (var client = new HttpClient())
        //    {
        //        //Passing service base url 
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Clear();
        //        //Define request data format  
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        //Sending request to find web api REST service resource Get using HttpClient  
        //        HttpResponseMessage Res = await client.GetAsync("api/Employees/");
        //        //Checking the response is successful or not which is sent using HttpClient  
        //        if (Res.IsSuccessStatusCode)
        //        {
        //            //Storing the response details recieved from web api   
        //            var EmpResponse = Res.Content.ReadAsStringAsync().Result;

        //            //Deserializing the response recieved from web api and storing into the Employee list  
        //            EmInfo = JsonConvert.DeserializeObject<List<Employees>>(EmpResponse);
        //        }
        //    }
        //    return View(EmInfo);
        //}

        public ActionResult Index()
        {
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:45370");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = _client.GetAsync("api/Employees").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Employees>>().Result;

            }
            else
            {
                ViewBag.result = "Error";
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNew(Employees employees)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                var result = client.PostAsJsonAsync("http://localhost:45370/api/Employees/CreateNew/", employees).Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Result = "Successfully saved!";
                    ModelState.Clear();
                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    ViewBag.Result = "ID is duplicated. Input ID again, please";
                }
            }
            return View(employees);
        }
    }
}