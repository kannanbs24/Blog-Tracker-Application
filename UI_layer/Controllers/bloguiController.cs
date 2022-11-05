using Dal_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using UI_layer.Models;

namespace UI_layer.Controllers
{
    public class bloguiController : Controller
    {
        // GET: blogui
        MyContext db = new MyContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(empmodel log)
        {
            var user = db.EmpInfos.Where(x => x.EmailId == log.EmailId && x.PassCode == log.PassCode).Count();
            if (user > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            List<blogmodel> emplist = new List<blogmodel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378//api/");

                var responseTask = client.GetAsync("blog");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<blogmodel[]>();
                    readData.Wait();
                    var empdata = readData.Result;
                    foreach (var item in empdata)
                    {
                        emplist.Add(new blogmodel
                        {
                            BlogId = item.BlogId,
                            Title = item.Title,
                            Subject = item.Subject,
                            DateOfCreation = item.DateOfCreation,
                            BlogUrl = item.BlogUrl,
                            EmpEmailId = item.EmpEmailId
                        });

                    }
                }
            }
            return View(emplist);

        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Create(blogmodel empmodel)
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378//api/blog");

                var emp = new blogmodel
                {
                    BlogId = empmodel.BlogId,
                    Title = empmodel.Title,
                    Subject = empmodel.Subject,
                   DateOfCreation = empmodel.DateOfCreation,
                   BlogUrl= empmodel.BlogUrl,
                   EmpEmailId= empmodel.EmpEmailId
                };

                var postTask = client.PostAsJsonAsync<blogmodel>(client.BaseAddress, emp);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtaskResult = result.Content.ReadAsAsync<blogmodel>();

                    readtaskResult.Wait();
                    var dataInserted = readtaskResult.Result;
                }


            }

            return RedirectToAction("Index");
        }
    }
}