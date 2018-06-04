using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using Recruitement.Models;
using System.Net.Http.Headers;

namespace Recruitement.Controllers
{
    public class CandidatesController : Controller
    {
        // GET: Candidates
        public ActionResult Index()
        {
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:24528");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = _client.GetAsync("api/Candidates").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Candidates>>().Result;
            }
            else
            {
                ViewBag.result = "Error";
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateNew(string id)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                Candidates nCandi = new Candidates();
                nCandi.Name = id;
                var result = client.PostAsJsonAsync("http://localhost:24528/api/Candidates/CreateNew/", nCandi).Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Result = "Successfully saved!";
                    ModelState.Clear();
                    return RedirectToAction("Index", "Candidates");
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.Result = "ID is duplicated. Input ID again, please";
                }
            }
            return RedirectToAction("Index", "Candidates");
        }
    }

}