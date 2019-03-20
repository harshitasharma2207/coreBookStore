using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreUser.Models;

namespace OnlineBookStoreUser.Controllers
{
    public class CustomersController : Controller
    {
        BookStoreContext context = new BookStoreContext();
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customers cust)
        {
            context.Customers.Add(cust);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Customers cust)
        {

           
            var obj = context.Customers.Where(a => a.UserName.Equals(cust.UserName) && a.Password.Equals(cust.Password)).FirstOrDefault();
            if (obj != null)
            {
                HttpContext.Session.SetString("uname", cust.UserName);
                return RedirectToAction("CheckOut", "Cart");
            }
            else
            {
                ViewBag.Error = "Invalid Credential";
                return View("Index");
            }

        }

    }
}
