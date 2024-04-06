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
    public class CartsController : Controller
    {
        private readonly MyDbContext _context;

        public CartsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Carts.Include(c => c.Customers);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.IdGioHang == id);
            if (carts == null)
            {
                return NotFound();
            }

            return View(carts);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["IDKH"] = new SelectList(_context.Customers, "IdKH", "DiaChi");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGioHang,IDKH")] Carts carts)
        {
            if (ModelState.IsValid)
            {
                carts.IdGioHang = Guid.NewGuid();
                _context.Add(carts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IDKH"] = new SelectList(_context.Customers, "IdKH", "DiaChi", carts.IDKH);
            return View(carts);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts.FindAsync(id);
            if (carts == null)
            {
                return NotFound();
            }
            ViewData["IDKH"] = new SelectList(_context.Customers, "IdKH", "DiaChi", carts.IDKH);
            return View(carts);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdGioHang,IDKH")] Carts carts)
        {
            if (id != carts.IdGioHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartsExists(carts.IdGioHang))
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
            ViewData["IDKH"] = new SelectList(_context.Customers, "IdKH", "DiaChi", carts.IDKH);
            return View(carts);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts
                .Include(c => c.Customers)
                .FirstOrDefaultAsync(m => m.IdGioHang == id);
            if (carts == null)
            {
                return NotFound();
            }

            return View(carts);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var carts = await _context.Carts.FindAsync(id);
            if (carts != null)
            {
                _context.Carts.Remove(carts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartsExists(Guid id)
        {
            return _context.Carts.Any(e => e.IdGioHang == id);
        }
    }
}
