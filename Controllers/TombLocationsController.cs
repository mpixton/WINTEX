using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WINTEX.DAL;
using WINTEX.Infrastructure;
using WINTEX.Models;

namespace WINTEX.Controllers
{
    public class TombLocationsController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public TombLocationsController(IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            this.diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: TombLocations
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            var list = _context.TombLocations;
            var pageInfo = new Paginator<TombLocation>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            return View(pageInfo.GetItems(pageNum));
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
        public async Task<IActionResult> Create([Bind("TombLocationId,AreaHillBurial,Tomb")] TombLocation tombLocation)
        {
            if (ModelState.IsValid)
            {
                tombLocation.LookupValue = $"{tombLocation.AreaHillBurial} {tombLocation.Tomb}";
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
        public async Task<IActionResult> Edit(int id, [Bind("TombLocationId,AreaHillBurial,Tomb")] TombLocation tombLocation)
        {
            if (id != tombLocation.TombLocationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tombLocation.LookupValue = $"{tombLocation.AreaHillBurial} {tombLocation.Tomb}";
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
