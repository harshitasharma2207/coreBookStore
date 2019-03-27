using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coreBookStoreUser.Helper;
using coreBookStoreUser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStoreUser.Models;

namespace OnlineBookStoreUser.Controllers
{
    public class BookController : Controller

    {
        BookStoreContext context = new BookStoreContext();
        public IActionResult BookCategoryIndex()
        {
            ViewBag.bookcategoryAuthor = context.Books.Include(c => c.Authors).ToList();
            ViewBag.bookcategoryPublication = context.Books.Include(p => p.Publication).ToList();
            ViewBag.bookcategoryBook = context.Books.ToList();
            return View();
        }

        public IActionResult Display(int id)
            
        {
            var books = context.Books.ToList();
            return View(books);
        }
        [Route("details")]
        public ActionResult Details(int id)
        {
            Books bk = context.Books.Where(x => x.BookId == id).SingleOrDefault();
            context.SaveChanges();
            return View(bk);
        }
    }
}