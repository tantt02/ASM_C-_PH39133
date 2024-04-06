using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_C__PH39133.Models;

namespace ASM_C__PH39133.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Watch(Guid? maSp)
        {
			if (maSp == null)
			{
				return NotFound();
			}
			var product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.MaSP == maSp);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Products.Include(p => p.Categories);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.MaSP == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["IdDanhMuc"] = new SelectList(_context.Categories, "IdDanhMuc", "MoTa");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSP,TenSP,IMEI,HinhAnh,SoLuong,DonGia,Mota,IdDanhMuc")] Products products)
        {
            if (ModelState.IsValid)
            {
                products.MaSP = Guid.NewGuid();
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDanhMuc"] = new SelectList(_context.Categories, "IdDanhMuc", "MoTa", products.IdDanhMuc);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["IdDanhMuc"] = new SelectList(_context.Categories, "IdDanhMuc", "MoTa", products.IdDanhMuc);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MaSP,TenSP,IMEI,HinhAnh,SoLuong,DonGia,Mota,IdDanhMuc")] Products products)
        {
            if (id != products.MaSP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.MaSP))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDanhMuc"] = new SelectList(_context.Categories, "IdDanhMuc", "MoTa", products.IdDanhMuc);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.MaSP == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(Guid id)
        {
            return _context.Products.Any(e => e.MaSP == id);
        }
    }
}
