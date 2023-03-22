using MaterEmergencyCareCentreApp.Domain.Models;
using MaterEmergencyCareCentreApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MaterEmergencyCareCentreApp.Controllers
{
    public class CommentController : Controller
    {
        // GET: CommentController
        // To View the Action result of create 
        public ActionResult Index()
        {
            //List<Comment> comments;
            //using (var httpClient = new HttpClient())
            //{
            //    using (var response = await httpClient.GetAsync("https://localhost:7058/Bed/GetComments?patientId=1"))
            //    {
            //        comments = await response.Content.ReadFromJsonAsync<List<Comment>>();
            //        //comments = JsonConvert.DeserializeObject<List<Comment>>(apiResponse) ?? new List<Comment>();
            //    }
            //}
            //return View(comments);
            return View();
        }

        // GET: CommentController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            List<Comment> comments;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7058/Bed/GetComments?patientId=" + id))
                {
                    comments = await response.Content.ReadFromJsonAsync<List<Comment>>();
                    //comments = JsonConvert.DeserializeObject<List<Comment>>(apiResponse) ?? new List<Comment>();
                }
            }
            return View(comments);
        }

        // GET: CommentController/Create
        // To View the Action result of create 
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: CommentController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CommentController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CommentController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: CommentController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
