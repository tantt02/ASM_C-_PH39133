using ASM_C__PH39133.Migrations;
using ASM_C__PH39133.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ASM_C__PH39133.Controllers
{
    public class Cart_Details : Controller
    {
        MyDbContext _dbContext;
        public Cart_Details()
        {
            _dbContext = new MyDbContext();
        }
        // GET: Cart_Details
        public ActionResult Index()
        {

            var idkh = HttpContext.Session.GetString("idKH");
            var c = _dbContext.Customers.Include(a => a.Carts).Include(a => a.Carts.Cart_Details).FirstOrDefault(a => a.IdKH == a.Carts.IDKH && a.IdKH == Guid.Parse(idkh));
            var data = _dbContext.CartDetails.Include(a => a.Products).Include(a => a.Carts).Where(a => a.Carts.IdGioHang == c.Carts.IdGioHang).ToList();
            if (idkh != null)
            {
                ViewBag.total = data.Sum(item => item.Products.DonGia * item.SoLuong);
                ViewBag.Count = data.Count;
            }
            return View(data);
        }

        // GET: Cart_Details/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult AddToCart(Guid idProduct)
        {

            var idkh = HttpContext.Session.GetString("idKH");
            if (idkh == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var c = _dbContext.Customers.Include(a => a.Carts).Include(a => a.Carts.Cart_Details).FirstOrDefault(a => a.IdKH == a.Carts.IDKH && a.IdKH == Guid.Parse(idkh));
            var data = _dbContext.CartDetails.Include(a => a.Products).Include(a => a.Carts).Where(a => a.Carts.IdGioHang == c.Carts.IdGioHang).ToList();

            var ac = c.Carts.Cart_Details.FirstOrDefault(cd => cd.MaSP == idProduct);
            if (ac == null)
            {
                var CartDetail = new Cart_detail()
                {
                    IdGioHangCT = Guid.NewGuid(),
                    IdGioHang = c.Carts.IdGioHang,
                    MaSP = idProduct,
                    SoLuong = 1
                };

                _dbContext.CartDetails.Add(CartDetail);
            }
            else
            {
                ac.SoLuong++;
            }
            _dbContext.SaveChanges();

            ViewBag.total = data.Sum(item => item.Products.DonGia * item.SoLuong);
            return RedirectToAction("Index");


        }
        // GET: Cart_Details/Edit/5

        public ActionResult Edit(Guid idProduct)
        {
            var a = _dbContext.CartDetails.Find(idProduct);
            ViewData["IdGioHang"] = new SelectList(_dbContext.Carts, "IdGioHang", "IdGioHang", a.IdGioHang);
            ViewData["MaSP"] = new SelectList(_dbContext.Products, "MaSP", "MaSP", a.MaSP);
            return View(a);
        }

        // POST: Cart_Details/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid idProduct, [Bind("IdGioHangCT,IdGioHang,MaSP,SoLuong")] Cart_detail cart_Detail)
        {
            //var delete = _dbContext.CartDetails.Find(cart_Detail.IdGioHangCT == idProduct);
            //delete.SoLuong = cart_Detail.SoLuong  ;
            //_dbContext.CartDetails.Update(delete);
            //_dbContext.SaveChanges();
            //return RedirectToAction("Index");
            Console.WriteLine(cart_Detail.IdGioHang.ToString().Length);

            _dbContext.CartDetails.Update(cart_Detail);
            _dbContext.SaveChanges();
  
            return RedirectToAction("Index");
        }

        // GET: Cart_Details/Delete/5
        public ActionResult Delete(Guid idProduct)
        {
            var delete = _dbContext.CartDetails.FirstOrDefault(a =>a.IdGioHangCT == idProduct );
            if (delete != null)
            {
                _dbContext.CartDetails.Remove(delete);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
