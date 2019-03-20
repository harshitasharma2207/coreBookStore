using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreBookStoreUser.Helper;
using coreBookStoreUser.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreUser.Models;

namespace OnlineBookStoreUser.Controllers
{
    public class BookController : Controller

    {
        BookStoreContext context = new BookStoreContext();
        public IActionResult BookCategoryIndex()
        {
            var bookcategory = context.Books.ToList();
            return View(bookcategory);
        }

        public IActionResult Display(int id)
            
        {
            var books = context.Books.ToList();
            return View(books);
        }
    }
}