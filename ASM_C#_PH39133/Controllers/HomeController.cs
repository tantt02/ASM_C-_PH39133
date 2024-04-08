using ASM_C__PH39133.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore;



namespace ASM_C__PH39133.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private MyDbContext _dbContext;
		public HomeController(ILogger<HomeController> logger)
		{
			_dbContext = new MyDbContext();

		}



		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(Customers customers)
		{
			var kq = _dbContext.Customers.FirstOrDefault(a => a.DienThoai == customers.DienThoai);
			if (kq == null)
			{
				var idkh = Guid.NewGuid();
				customers.IdKH = idkh;
				string sdt = customers.DienThoai;
				string pattern = @"^\d{10}$";
				if (Regex.IsMatch(sdt, pattern))
				{
					_dbContext.Customers.Add(customers);
					var cart = new Carts()
					{
						IdGioHang = Guid.NewGuid(),
						IDKH = idkh,
					};
					_dbContext.Carts.Add(cart);
					_dbContext.SaveChanges();

					return RedirectToAction("Login");
				}
				else
				{
					ViewBag.checkSDT = "Số điện thoại gồm 10 chữ số";
					return View("Register");
				}
			}
			else
			{
				ViewBag.Error = "SĐT đã tồn tại";
				return View("Register");
			}
		}
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(Customers kh)
		{
			var a = _dbContext.Customers.ToList();
			var kq = a.FirstOrDefault(a => a.DienThoai == kh.DienThoai && a.MatKhau == kh.MatKhau);
			if (kq != null)
			{
				HttpContext.Session.SetString("Username", kq.TenKH.ToString());
				HttpContext.Session.SetString("idKH", kq.IdKH.ToString());
				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng";
				return View("Login");
			}
		}

		[HttpPost]
		public IActionResult Logout()
		{

			HttpContext.Session.Remove("Username");
			return RedirectToAction("Index");


		}


		public IActionResult Index(Guid? categoryId, string name)
		{
			var products = _dbContext.Products.ToList();
			var findProducts = _dbContext.Products.Where(a => a.TenSP.Contains(name)).ToList();
			if (findProducts.Count != 0)
			{
				return View(findProducts);
			}

			if (categoryId.HasValue) // hasValue là nullable (Guid) 
			{
				products = _dbContext.Products.Where(p => p.IdDanhMuc == categoryId).ToList();
			}
			else
			{
				products = _dbContext.Products.ToList();
			}

			return View(products);

		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
