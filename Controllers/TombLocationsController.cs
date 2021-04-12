using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WINTEX.DAL;
using WINTEX.Models;

namespace WINTEX.Controllers
{
    public class TombLocationsController : Controller
    {
        private readonly FEGBExcavationContext _context;

        public TombLocationsController(FEGBExcavationContext context)
        {
            _context = context;
        }

        // GET: TombLocations
        public async Task<IActionResult> Index()
        {
            return View(await _context.TombLocations.ToListAsync());
        }

        // GET: TombLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tombLocation = await _context.TombLocations
                .FirstOrDefaultAsync(m => m.TombLocationId == id);
            if (tombLocation == null)
            {
                return NotFound();
            }

            return View(tombLocation);
        }

        // GET: TombLocations/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TombLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("TombLocationId,LookupValue,AreaHillBurial,Tomb")] TombLocation tombLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tombLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tombLocation);
        }

        // GET: TombLocations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tombLocation = await _context.TombLocations.FindAsync(id);
            if (tombLocation == null)
            {
                return NotFound();
            }
            return View(tombLocation);
        }

        // POST: TombLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TombLocationId,LookupValue,AreaHillBurial,Tomb")] TombLocation tombLocation)
        {
            if (id != tombLocation.TombLocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tombLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TombLocationExists(tombLocation.TombLocationId))
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
            return View(tombLocation);
        }

        // GET: TombLocations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tombLocation = await _context.TombLocations
                .FirstOrDefaultAsync(m => m.TombLocationId == id);
            if (tombLocation == null)
            {
                return NotFound();
            }

            return View(tombLocation);
        }

        // POST: TombLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tombLocation = await _context.TombLocations.FindAsync(id);
            _context.TombLocations.Remove(tombLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TombLocationExists(int id)
        {
            return _context.TombLocations.Any(e => e.TombLocationId == id);
        }
    }
}
