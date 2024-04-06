using ASM_C__PH39133.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("Db_C#4"));
});
// Add services to the container.
builder.Services.AddControllersWithViews();
//Thêm dòng lệnh
builder.Services.AddSession();
builder.Services.AddDbContext<MyDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//Thêm dòng lệnh
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

using (var dbContext = new MyDbContext())
{
	var a = dbContext.Customers.FirstOrDefault(c => c.IsAdmin);
	if (a != null)
	{
		dbContext.Customers.Remove(a);
	}
	var admin = new Customers()
	{
		IdKH = Guid.NewGuid(),
		DienThoai = "0378990907",
		TenKH = "Trọng Tấn",
		DiaChi = "Hà Nội",
		GioiTinh = "Nam",
		MatKhau = "123",
		IsAdmin = true,
	};
	var cart = new Carts()
	{
		IdGioHang = Guid.NewGuid(),
		IDKH = admin.IdKH
	};
	dbContext.Customers.Add(admin);
	dbContext.Carts.Add(cart);
	dbContext.SaveChanges();
}
	app.Run();
