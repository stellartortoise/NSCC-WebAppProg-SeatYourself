using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NSCC_WebAppProg_SeatYourself.Data;
using NSCC_WebAppProg_SeatYourself.Models;

namespace NSCC_WebAppProg_SeatYourself.Controllers
{
    public class OccasionsController : Controller
    {
        private readonly NSCC_WebAppProg_SeatYourselfContext _context;

        public OccasionsController(NSCC_WebAppProg_SeatYourselfContext context)
        {
            _context = context;
        }

        // GET: Occasions
        public async Task<IActionResult> Index()
        {
            var nSCC_WebAppProg_SeatYourselfContext = _context.Occasion.Include(o => o.Category).Include(o => o.Venue);
            return View(await nSCC_WebAppProg_SeatYourselfContext.ToListAsync());
        }

        // GET: Occasions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occasion = await _context.Occasion
                .Include(o => o.Category)
                .Include(o => o.Venue)
                .FirstOrDefaultAsync(m => m.OccasionId == id);
            if (occasion == null)
            {
                return NotFound();
            }

            return View(occasion);
        }

        // GET: Occasions/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Name");
            return View();
        }

        // POST: Occasions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OccasionId,Title,Description,Date,Time,Owner,VenueId,CategoryId")] Occasion occasion)
        {
            occasion.CreatedAt = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(occasion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Name");
            return View(occasion);
        }

        // GET: Occasions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occasion = await _context.Occasion.FindAsync(id);
            if (occasion == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", occasion.CategoryId);
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Name", occasion.VenueId);
            return View(occasion);
        }

        // POST: Occasions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OccasionId,Title,Description,Date,Time,Owner,VenueId,CategoryId")] Occasion occasion)
        {
            if (id != occasion.OccasionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    occasion.CreatedAt = DateTime.Now; // This will put the edited date/time, to keep the original created date/time it must be put into a variable? leaving this for now
                    _context.Update(occasion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccasionExists(occasion.OccasionId))
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
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", occasion.CategoryId);
            ViewData["VenueId"] = new SelectList(_context.Set<Venue>(), "VenueId", "Name", occasion.VenueId);
            return View(occasion);
        }

        // GET: Occasions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occasion = await _context.Occasion
                .Include(o => o.Category)
                .Include(o => o.Venue)
                .FirstOrDefaultAsync(m => m.OccasionId == id);
            if (occasion == null)
            {
                return NotFound();
            }

            return View(occasion);
        }

        // POST: Occasions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var occasion = await _context.Occasion.FindAsync(id);
            if (occasion != null)
            {
                _context.Occasion.Remove(occasion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccasionExists(int id)
        {
            return _context.Occasion.Any(e => e.OccasionId == id);
        }
    }
}
