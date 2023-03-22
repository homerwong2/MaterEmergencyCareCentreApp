using MaterEmergencyCareCentreApp.Configuration;
using MaterEmergencyCareCentreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MaterEmergencyCareCentreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<APIOptions> _options;

        public HomeController(ILogger<HomeController> logger, IOptions<APIOptions> options)
        {
            _logger = logger;
            _options = options;
        }

        public async Task<IActionResult> Index()
        {
            List<BedDtoViewModel> beds = new List<BedDtoViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7058/Bed/GetBeds"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    beds = JsonConvert.DeserializeObject<List<BedDtoViewModel>>(apiResponse);
                }
            }
            return View(beds);
        }

        // https://localhost:7281/Home/AddComment/1
        [HttpGet]
        [Route("Home/AddComment/{patientId:int}%2F{patient}")]
        public IActionResult AddComment(int patientId, string patient)
        {
            return View(new CommentDtoViewModel(patientId, patient));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}