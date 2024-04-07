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
    public class Cart_detailController : Controller
    {
        private readonly MyDbContext _context;

        public Cart_detailController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Cart_detail
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.CartDetails.Include(c => c.Carts).Include(c => c.Products);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Cart_detail/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart_detail = await _context.CartDetails
                .Include(c => c.Carts)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.IdGioHangCT == id);
            if (cart_detail == null)
            {
                return NotFound();
            }

            return View(cart_detail);
        }

        // GET: Cart_detail/Create
        public IActionResult Create()
        {
            ViewData["IdGioHang"] = new SelectList(_context.Carts, "IdGioHang", "IdGioHang");
            ViewData["MaSP"] = new SelectList(_context.Products, "MaSP", "HinhAnh");
            return View();
        }

        // POST: Cart_detail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGioHangCT,IdGioHang,MaSP,SoLuong")] Cart_detail cart_detail)
        {
            if (ModelState.IsValid)
            {
                cart_detail.IdGioHangCT = Guid.NewGuid();
                _context.Add(cart_detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGioHang"] = new SelectList(_context.Carts, "IdGioHang", "IdGioHang", cart_detail.IdGioHang);
            ViewData["MaSP"] = new SelectList(_context.Products, "MaSP", "HinhAnh", cart_detail.MaSP);
            return View(cart_detail);
        }

        // GET: Cart_detail/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart_detail = await _context.CartDetails.FindAsync(id);
            if (cart_detail == null)
            {
                return NotFound();
            }
            ViewData["IdGioHang"] = new SelectList(_context.Carts, "IdGioHang", "IdGioHang", cart_detail.IdGioHang);
            ViewData["MaSP"] = new SelectList(_context.Products, "MaSP", "HinhAnh", cart_detail.MaSP);
            return View(cart_detail);
        }

        // POST: Cart_detail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdGioHangCT,IdGioHang,MaSP,SoLuong")] Cart_detail cart_detail)
        {
            if (id != cart_detail.IdGioHangCT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart_detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Cart_detailExists(cart_detail.IdGioHangCT))
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
            ViewData["IdGioHang"] = new SelectList(_context.Carts, "IdGioHang", "IdGioHang", cart_detail.IdGioHang);
            ViewData["MaSP"] = new SelectList(_context.Products, "MaSP", "HinhAnh", cart_detail.MaSP);
            return View(cart_detail);
        }

        // GET: Cart_detail/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart_detail = await _context.CartDetails
                .Include(c => c.Carts)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.IdGioHangCT == id);
            if (cart_detail == null)
            {
                return NotFound();
            }

            return View(cart_detail);
        }

        // POST: Cart_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cart_detail = await _context.CartDetails.FindAsync(id);
            if (cart_detail != null)
            {
                _context.CartDetails.Remove(cart_detail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Cart_detailExists(Guid id)
        {
            return _context.CartDetails.Any(e => e.IdGioHangCT == id);
        }
    }
}
