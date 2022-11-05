using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using UI_layer.Models;
using Dal_lib;
namespace UI_layer.Controllers
{
    public class empuiController : Controller
    {
        // GET: empui
     MyContext db=new MyContext(); 
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(Adminmodel log)
        {
            var user = db.AdminInfos.Where(x => x.EmailId == log.EmailId && x.Password == log.Password).Count();
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
            List<empmodel> emplist = new List<empmodel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378//api/");

                var responseTask = client.GetAsync("employee");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readData = result.Content.ReadAsAsync<empmodel[]>();
                    readData.Wait();
                    var empdata = readData.Result;
                    foreach (var item in empdata)
                    {
                        emplist.Add(new empmodel
                        {
                            EmailId = item.EmailId,
                            Name = item.Name,
                            DateOfJoining = item.DateOfJoining,
                            PassCode = item.PassCode
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

        public ActionResult Create(empmodel empmodel)
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378//api/employee");

                var emp = new empmodel
                {
                    EmailId = empmodel.EmailId,
                    Name = empmodel.Name,
                    DateOfJoining = empmodel.DateOfJoining,
                    PassCode = empmodel.PassCode
                };

                var postTask = client.PostAsJsonAsync<empmodel>(client.BaseAddress, emp);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtaskResult = result.Content.ReadAsAsync<empmodel>();

                    readtaskResult.Wait();
                    var dataInserted = readtaskResult.Result;
                }


            }

            return RedirectToAction("Index");
        }

    }
}