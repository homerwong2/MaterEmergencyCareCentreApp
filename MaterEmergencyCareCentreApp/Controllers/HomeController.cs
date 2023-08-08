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
using Microsoft.VisualBasic;
using System.Net;
using System.Web;
using MaterEmergencyCareCentreApp.Domain.Models;
using System.Security.Policy;
using System.Text;
using System.Net.Http;
using MaterEmergencyCareCentreApp.Domain.DTOs;

namespace MaterEmergencyCareCentreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<APIOptions> _options;
        private readonly HttpClient _httpClient;

        public HomeController(
                ILogger<HomeController> logger, 
                IOptions<APIOptions> options, 
                IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _options = options;

            // HttpClient is intended to be instantiated once and reused throughout the life of an application.
            // Instantiating an HttpClient class for every request will exhaust available sockets.  
            // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0
            _httpClient = httpClientFactory.CreateClient(); ;
        }

        // For example: https://localhost:7281/
        public async Task<IActionResult> Index()
        { 
            SummaryViewModel summary = new SummaryViewModel()
            {
                Beds = await GetBedsAsync(),
                CountOfBedsInUse = await CountBedsInUseAsync(),
                CountOfBedsFree = await CountBedsFreeAsync(),
                CountTotalPatientsAdmittedToday = await CountTotalPatientsAdmittedTodayAsync(),
                AdmittedPatientsUsingABed = await GetAdmittedPatientsUsingABedAsync()
            };

            return View(summary);
        }

        public async Task<List<BedDtoViewModel>> GetBedsAsync()
        {
            List<BedDtoViewModel> beds = new List<BedDtoViewModel>();
            using (var response = await _httpClient.GetAsync(_options.Value.BaseURL + "/Bed/GetBeds"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                beds = JsonConvert.DeserializeObject<List<BedDtoViewModel>>(apiResponse);
            }

            return beds;
        }

        public async Task<int> CountBedsInUseAsync()
        {
            int countOfBedsInUse = 0;
            using (var response = await _httpClient.GetAsync(_options.Value.BaseURL + "/Bed/CountBedsInUse"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                countOfBedsInUse = JsonConvert.DeserializeObject<int>(apiResponse);
            }
            return countOfBedsInUse;
        }

        public async Task<int> CountBedsFreeAsync()
        {
            int countOfBedsFree = 0;
            using (var response = await _httpClient.GetAsync(_options.Value.BaseURL + "/Bed/CountBedsFree"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                countOfBedsFree = JsonConvert.DeserializeObject<int>(apiResponse);
            }
            return countOfBedsFree;
        }

        public async Task<int> CountTotalPatientsAdmittedTodayAsync()
        {
            int countOfTotalPatientsAdmittedToday = 0;
            using (var response = await _httpClient.GetAsync(_options.Value.BaseURL + "/Bed/CountTotalPatientsAdmittedToday"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                countOfTotalPatientsAdmittedToday = JsonConvert.DeserializeObject<int>(apiResponse);
            }
            return countOfTotalPatientsAdmittedToday;
        }

        public async Task<List<Patient>> GetAdmittedPatientsUsingABedAsync()
        {
            List<Patient> patients = new List<Patient>();
            using (var response = await _httpClient.GetAsync(_options.Value.BaseURL + "/Bed/GetAdmittedPatientsUsingABed"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                patients = JsonConvert.DeserializeObject<List<Patient>>(apiResponse);
            }

            return patients;
        }

        // For example: https://localhost:7281/Home/AddComment/1%2FJohn Doe
        [HttpGet]
        [Route("Home/AddComment/{patientId:int}%2F{patient}")]
        public IActionResult AddComment(int patientId, string patient)
        {
            return View(new CommentDto()
            {
                PatientId = patientId, 
                Patient = patient,
                CommentTime = DateTime.Now
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(CommentDto commentDto)
        {
            if (ModelState.IsValid && commentDto != null)
            {
                // Convert any HTML markup in the Comment text.
                commentDto.Text = HttpUtility.HtmlEncode(commentDto.Text);
                commentDto.Nurse = HttpUtility.HtmlEncode(commentDto.Nurse);

                var json = JsonConvert.SerializeObject(commentDto);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                bool result;

                using (var response = await _httpClient.PostAsync(_options.Value.BaseURL + "/Bed/AddComment", stringContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            try {
                    return RedirectToAction(nameof(Index));
            } catch {
                    return View();
            }            
        }

        // For example: https://localhost:7281/Home/AdmitPatient/1
        [HttpGet]
        [Route("Home/AdmitPatient/{bedId:int}")]
        public IActionResult AdmitPatient(int bedId)
        {
            return View(new PatientDto()
            {
                BedId = bedId,
                DateAdmitted = DateTime.Now
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostAdmission(PatientDto patientDto)
        {
            if (ModelState.IsValid && patientDto != null)
            {
                // Convert any HTML markup in the text.
                patientDto.Name = HttpUtility.HtmlEncode(patientDto.Name);
                patientDto.PresentingIssue = HttpUtility.HtmlEncode(patientDto.PresentingIssue);
                patientDto.Nurse = HttpUtility.HtmlEncode(patientDto.Nurse);

                var json = JsonConvert.SerializeObject(patientDto);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json"); 
                bool result;

                using (var response = await _httpClient.PostAsync(_options.Value.BaseURL + "/Bed/AdmitPatient", stringContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // For example: https://localhost:7281/Home/DischargePatient/1%2FJohn Doe
        [HttpGet]
        [Route("Home/DischargePatient/{patientId:int}%2F{patient}")]
        public IActionResult DischargePatient(int patientId, string patient)
        {
            return View(new DischargeDto()
            {
                PatientId = patientId,
                Patient = patient
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostDischarge(DischargeDto dischargeDto)
        {
            if (ModelState.IsValid && dischargeDto != null)
            {
                // Convert any HTML markup in the text.
                dischargeDto.Nurse = HttpUtility.HtmlEncode(dischargeDto.Nurse);

                var json = JsonConvert.SerializeObject(dischargeDto);
                var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
                bool result;

                using (var response = await _httpClient.PostAsync(_options.Value.BaseURL + "/Bed/DischargePatient", stringContent))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(apiResponse);
                }
            }

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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