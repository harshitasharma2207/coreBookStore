

using coreBookStoreUser.Helper;
using coreBookStoreUser.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStoreUser.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBookStoreUser.Controllers
{
    public class CartController : Controller
    {
        BookStoreContext context = new BookStoreContext();
        [Route("index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int i = 0;

            if (cart != null)
            {
                foreach (var item in cart)
                {
                    i++;
                }
                if (i != 0)
                {
                    ViewBag.cart = cart;
                    ViewBag.total = cart.Sum(item => item.Books.BookPrice * item.Quantity);
                    return View();
                }
            }
                                  
            return View("EmptyCart");


        }

        [Route("buy/{id}")]
        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item
                {
                    Books = context.Books.Find(id),
                    Quantity = 1
                });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item
                    {
                        Books = context.Books.Find(id),
                        Quantity = 1
                    });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }


            return RedirectToAction("Index", "Home");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            int i = 0;
            foreach (var item in cart)
            {
                i++;
            }
            if (i != 0)
            {
                int j = int.Parse(HttpContext.Session.GetString("cart"));
                j--;
                HttpContext.Session.SetString("cart", j.ToString());
            }
            else
            {
                HttpContext.Session.Remove("cart");
                return View("EmptyCart");

            }
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Books.BookId.Equals(id))
                {
                    return i;
                }

            }
            return -1;
        }
        public ActionResult Details(int id)
        {
            Books bk = context.Books.Where(x => x.BookId == id).SingleOrDefault();
            context.SaveChanges();
            return View(bk);
        }


        [HttpGet]
        public ActionResult CheckOut()
        {
            int i = 0;
            ViewBag.i = i;
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Books.BookPrice * item.Quantity);
            TempData["total"] = ViewBag.total;
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(Customers customers)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Add(customers);
                context.SaveChanges();
                
                Order order = new Order()
                {
                    OrderAmount = Convert.ToSingle(TempData["total"]),
                    OrderDate = DateTime.Now,
                    CustomerId = customers.CustomerId
                };
                context.Order.Add(order);
                context.SaveChanges();
                //    return RedirectToAction("Payment");
                //}
                //return View(customers);

                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                List<OrderBook> orderBooks = new List<OrderBook>();
                for (int i = 0; i < cart.Count; i++)
                {
                    OrderBook orderBook = new OrderBook()
                    {
                        OrderId = order.OrderId,
                        BookId = cart[i].Books.BookId,
                        Quantity = cart[i].Quantity
                    };
                    orderBooks.Add(orderBook);
                }
                orderBooks.ForEach(n => context.OrderBook.Add(n));
                context.SaveChanges();
                TempData["cust"] = customers.CustomerId;
                return RedirectToAction("Invoice", "Cart");
            }

            return View(customers);

        }

        public IActionResult EmptyCart()
        {
            return View();
        }

        public IActionResult Invoice()
        {
            int CustId = int.Parse(TempData["cust"].ToString());
            Customers customers = context.Customers.Where(x => x.CustomerId == CustId).SingleOrDefault();
            ViewBag.Customers = customers;
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Books.BookPrice * item.Quantity);
            TempData["total"] = ViewBag.total;
            return View();


        }
    }
}
