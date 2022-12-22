using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using QuizMVC.Models;

namespace QuizMVC.Controllers
{
    public class QuizDetailController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Quizdetail> quizdetails = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5195/api/");
                var responseTask = client.GetAsync("Quizdetail");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<List<Quizdetail>>();
                    readJob.Wait();
                    quizdetails = readJob.Result;
                }
                else
                {
                    quizdetails = Enumerable.Empty<Quizdetail>();
                    ModelState.AddModelError(String.Empty, "Server Error");
                }
                return View(quizdetails);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Quizdetail quizdetail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5195/api/Quizdetail");
                var postjob = client.PostAsJsonAsync<Quizdetail>("Quizdetail", quizdetail);
                postjob.Wait();

                var postresult = postjob.Result;
                if (postresult.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server Error");

                return View(quizdetail);
            }
        }
        public ActionResult Edit(int id)
        {
            Quizdetail quizdetail = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5195/api/");
                var responseTask = client.GetAsync("Quizdetail/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<Quizdetail>();
                    readtask.Wait();

                    quizdetail = readtask.Result;
                }
            }
            return View(quizdetail);
        }

        [HttpPost]
        public ActionResult Edit(Quizdetail quizdetail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5195/api/Quizdetail");
                var puttask = client.PutAsJsonAsync<Quizdetail>("Quizdetail", quizdetail);
                puttask.Wait();

                var result = puttask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(quizdetail);
            }
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5195/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Quizdetail/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}