using ASM_C__PH39133.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


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
			return View();
		}

		// GET: Cart_Details/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Cart_Details/Create
		//public ActionResult Create()
		//{
		//    return View();
		//}

		// POST: Cart_Details/Create


		public ActionResult AddToCart(Guid idProduct)
		{

			var idkh = HttpContext.Session.GetString("idKH");
			ViewBag.Id = idkh;
			if (idkh == null)
			{
				return RedirectToAction("Login", "Home");
			}
			var c = _dbContext.Customers.Include(a => a.Carts).ThenInclude(a => a.Cart_Details).FirstOrDefault(a => a.IdKH == a.Carts.IDKH && a.IdKH == Guid.Parse(idkh));

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

				c.Carts.Cart_Details.Add(CartDetail);
			}
			else
			{
				ac.SoLuong++;
			}
			//try
			//{
			//	// Lưu thay đổi vào cơ sở dữ liệu
			//	_dbContext.SaveChanges();
			//}
			//catch (DbUpdateConcurrencyException ex)
			//{
			//	foreach (var entry in ex.Entries)
			//	{
			//		if (entry.Entity is Cart_detail cartDetail)
			//		{
			//			_dbContext.Entry(cartDetail).Reload();
			//			// Điều chỉnh thay đổi của bạn trên đối tượng đã tải lại
			//		}
			//	}
			//}

			return View("listCarts");


		}
		// GET: Cart_Details/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Cart_Details/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Cart_Details/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Cart_Details/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
