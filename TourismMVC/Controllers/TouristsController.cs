using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TourismMVC.Models;

namespace TourismMVC.Controllers
{
    public class TouristsController : Controller
    {
        private readonly TourismContext _context;

        public TouristsController(TourismContext context)
        {
            _context = context;
        }

        // GET: Tourists
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null || name == null)
            {
                return RedirectToAction("Index", "Countries");
            }
            ViewBag.CountryId = id;
            ViewBag.CountryName = name;
            var tourismContext = _context.Tourists.Where(p => p.CountryId == id);
            return View(await tourismContext.ToListAsync());
        }

        // GET: Tourists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tourists == null)
            {
                return NotFound();
            }

            var tourist = await _context.Tourists
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourist == null)
            {
                return NotFound();
            }

            return View(tourist);
        }

        // GET: Tourists/Create
        public IActionResult Create(int id)
        {
            ViewBag.CountryId = id;
            ViewBag.CountryName = _context.Countries.First(t => t.Id == id).Name;
            return View();
        }

        // POST: Tourists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int CountryId, [Bind("Name,Surname,CountryId")] Tourist tourist)
        {
            tourist.CountryId = CountryId;
            if (ModelState.IsValid)
            {
                _context.Add(tourist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = CountryId, name = _context.Countries.First(t => t.Id == CountryId).Name });
            }
            ViewBag.CountryId = CountryId;
            ViewBag.CountryName = _context.Countries.First(t => t.Id == CountryId).Name;
            return View(tourist);
        }

        // GET: Tourists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tourists == null)
            {
                return NotFound();
            }

            var tourist = await _context.Tourists.FindAsync(id);
            if (tourist == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", tourist.CountryId);
            return View(tourist);
        }

        // POST: Tourists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,CountryId")] Tourist tourist)
        {
            if (id != tourist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TouristExists(tourist.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", tourist.CountryId);
            return View(tourist);
        }

        // GET: Tourists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tourists == null)
            {
                return NotFound();
            }

            var tourist = await _context.Tourists
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourist == null)
            {
                return NotFound();
            }

            return View(tourist);
        }

        // POST: Tourists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tourists == null)
            {
                return Problem("Entity set 'TourismContext.Tourists'  is null.");
            }
            var tourist = await _context.Tourists.FindAsync(id);
            if (tourist != null)
            {
                _context.Tourists.Remove(tourist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TouristExists(int id)
        {
          return _context.Tourists.Any(e => e.Id == id);
        }
    }
}
