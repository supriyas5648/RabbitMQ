using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitMQ.Client;
using MVC;
using Microsoft.AspNetCore.Mvc;
namespace ChatApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepo _userRepo;
        
        public HomeController(UserRepo _userRepo)
        {
            this._userRepo = _userRepo;
        }

        public ActionResult Index()
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            if (userid == null)
            {
                return RedirectToAction("login");
            }
            
           return View();
            
        }

        // [HttpPost]
        // public JsonResult sendmsg(string message, string friend)
        // {
        //     RabbitMQBll obj = new RabbitMQBll();
        //     IConnection con = obj.GetConnection();
        //     bool flag = obj.send(con, message, friend);
        //     return Json(null);
        // }
        // [HttpPost]
        // public JsonResult receive()
        // {
        //     try
        //     {
        //         RabbitMQBll obj = new RabbitMQBll();
        //         IConnection con = obj.GetConnection();
        //         string userqueue = Session["username"].ToString();
        //         string message = obj.receive(con, userqueue);
        //         return Json(message);
        //     }
        //     catch (Exception)
        //     {
        //         return null;
        //     }
        // }

        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(FormCollection fc)
        {
            string email = fc["txtemail"].ToString();
            string password = fc["txtpassword"].ToString();

            UserModel user = _userRepo.login(email, password);

            if (user.UserId > 0)
            {
                ViewData["status"] = 1;
                ViewData["msg"] = "login Successful...";
                HttpContext.Session.SetString("UserEmail" , user.UserEmail);
                HttpContext.Session.SetInt32("UserId" , user.UserId);

                return RedirectToAction("Index");
            }
            else
            {
                ViewData["status"] = 2;
                ViewData["msg"] = "invalid Email or Password...";
                return View();
            }
        }
        // [HttpPost]
        // public JsonResult friendlist()
        // {
        //     int id = Convert.ToInt32(Session["userid"].ToString());
        //     List<UserModel> users = dl.getusers(id);
        //     List<ListItem> userlist = new List<ListItem>();
        //     foreach (var item in users)
        //     {
        //         userlist.Add(new ListItem
        //         {
        //             Value = item.email.ToString(),
        //             Text = item.email.ToString()
        //         });
        //     }
        //     return Json(userlist);
        // }
    }
}