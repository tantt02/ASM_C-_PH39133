using ASM_C__PH39133.Migrations;
using ASM_C__PH39133.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
			return View("Index", data);


		}
		// GET: Cart_Details/Edit/5
		
		public ActionResult Edit(Guid idProduct)
        {
			var idkh = HttpContext.Session.GetString("idKH");
			var c = _dbContext.Customers.Include(a => a.Carts).Include(a => a.Carts.Cart_Details).FirstOrDefault(a => a.IdKH == a.Carts.IDKH && a.IdKH == Guid.Parse(idkh));
			var data = _dbContext.CartDetails.Include(a => a.Products).Include(a => a.Carts).Where(a => a.Carts.IdGioHang == c.Carts.IdGioHang).ToList();
			var ac = c.Carts.Cart_Details.FirstOrDefault(cd => cd.MaSP == idProduct);
			return View(ac);
		}

		// POST: Cart_Details/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid idProduct, string a)
		{
			var idkh = HttpContext.Session.GetString("idKH");
			var c = _dbContext.Customers.Include(a => a.Carts).Include(a => a.Carts.Cart_Details).FirstOrDefault(a => a.IdKH == a.Carts.IDKH && a.IdKH == Guid.Parse(idkh));
			var data = _dbContext.CartDetails.Include(a => a.Products).Include(a => a.Carts).Where(a => a.Carts.IdGioHang == c.Carts.IdGioHang).ToList();
			var ac = c.Carts.Cart_Details.FirstOrDefault(cd => cd.MaSP == idProduct);
			if (ac != null)
			{
				//_dbContext.CartDetails.Find(ac);
				_dbContext.CartDetails.Update(ac);
				_dbContext.SaveChanges();
			}

			return View("Index", data);

		}

		// GET: Cart_Details/Delete/5
		public ActionResult Delete(Guid idProduct)
		{
			var idkh = HttpContext.Session.GetString("idKH");
			var c = _dbContext.Customers.Include(a => a.Carts).Include(a => a.Carts.Cart_Details).FirstOrDefault(a => a.IdKH == a.Carts.IDKH && a.IdKH == Guid.Parse(idkh));
			var data = _dbContext.CartDetails.Include(a => a.Products).Include(a => a.Carts).Where(a => a.Carts.IdGioHang == c.Carts.IdGioHang).ToList();
			var ac = c.Carts.Cart_Details.FirstOrDefault(cd => cd.MaSP == idProduct);
			if (ac != null)
			{
				_dbContext.CartDetails.Remove(ac);
				_dbContext.SaveChanges();
			}
			return RedirectToAction(nameof(Index));
		}

	}
}
