using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using coreBookStoreUser.Models;
using OnlineBookStoreUser.Models;
using coreBookStoreUser.Helper;
using Microsoft.AspNetCore.Http;

namespace coreBookStoreUser.Controllers
{
    public class HomeController : Controller
    {
        BookStoreContext context = new BookStoreContext();
        public IActionResult Index()
        {
            var bookcategory = context.Books.ToList();
            return View(bookcategory);

        }
        public IActionResult CategoryDisplay()
        {
            var bookcategories = context.BookCategories.ToList();
            int i = 0;
            int j = 0;
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    i++;
                }
                if (i != 0)
                {
                    foreach (var item in cart)
                    {
                        j++;
                    }
                }
                HttpContext.Session.SetString("CartItem", i.ToString());
            }
            return View(bookcategories);


        }
    }
}
