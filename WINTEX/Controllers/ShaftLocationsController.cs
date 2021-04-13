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
using WINTEX.Enums;
using WINTEX.Infrastructure;
using WINTEX.Models;

namespace WINTEX.Controllers
{
    public class ShaftLocationsController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public ShaftLocationsController(IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            this.diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: ShaftLocations
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            var list = _context.ShaftLocations;
            var pageInfo = new Paginator<ShaftLocation>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            return View(pageInfo.GetItems(pageNum));
        }

        // GET: ShaftLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shaftLocation = await _context.ShaftLocations
                .FirstOrDefaultAsync(m => m.ShaftId == id);
            if (shaftLocation == null)
            {
                return NotFound();
            }

            return View(shaftLocation);
        }

        // GET: ShaftLocations/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            //ViewData["TombId"] = new SelectList(_context.TombLocations, "TombLocationId", "LookupValue", mummy.TombId);
            ViewBag.NorthSouth = new SelectList(CardinalDirections.NorthSouth());
            ViewBag.EastWest = new SelectList(CardinalDirections.EastWest());
            ViewBag.Subplots = new SelectList(CardinalDirections.SubPlots());
            return View();
        }

        // POST: ShaftLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("ShaftId,Ylower,Yupper,North,Xlower,Xupper,East,Subplot")] ShaftLocation shaftLocation)
        {
            if (ModelState.IsValid)
            {
                shaftLocation.Lookup = $"{shaftLocation.Xlower}/{shaftLocation.Xupper} {shaftLocation.North} {shaftLocation.Ylower}/{shaftLocation.Yupper} {shaftLocation.East} {shaftLocation.Subplot}";
                _context.Add(shaftLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.NorthSouth = new SelectList(CardinalDirections.NorthSouth(), shaftLocation.North);
            ViewBag.EastWest = new SelectList(CardinalDirections.EastWest(), shaftLocation.East);
            ViewBag.Subplots = new SelectList(CardinalDirections.SubPlots(), shaftLocation.Subplot);
            return View(shaftLocation);
        }

        // GET: ShaftLocations/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shaftLocation = await _context.ShaftLocations.FindAsync(id);
            if (shaftLocation == null)
            {
                return NotFound();
            }
            ViewBag.NorthSouth = new SelectList(CardinalDirections.NorthSouth(), shaftLocation.North);
            ViewBag.EastWest = new SelectList(CardinalDirections.EastWest(), shaftLocation.East);
            ViewBag.Subplots = new SelectList(CardinalDirections.SubPlots(), shaftLocation.Subplot);
            return View(shaftLocation);
        }

        // POST: ShaftLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int id, [Bind("ShaftId,Ylower,Yupper,North,Xlower,Xupper,East,Subplot")] ShaftLocation shaftLocation)
        {
            if (id != shaftLocation.ShaftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    shaftLocation.Lookup = $"{shaftLocation.Xlower}/{shaftLocation.Xupper} {shaftLocation.North} {shaftLocation.Ylower}/{shaftLocation.Yupper} {shaftLocation.East} {shaftLocation.Subplot}";
                    _context.Update(shaftLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShaftLocationExists(shaftLocation.ShaftId))
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
            ViewBag.NorthSouth = new SelectList(CardinalDirections.NorthSouth(), shaftLocation.North);
            ViewBag.EastWest = new SelectList(CardinalDirections.EastWest(), shaftLocation.East);
            ViewBag.Subplots = new SelectList(CardinalDirections.SubPlots(), shaftLocation.Subplot);
            return View(shaftLocation);
        }

        // GET: ShaftLocations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shaftLocation = await _context.ShaftLocations
                .FirstOrDefaultAsync(m => m.ShaftId == id);
            if (shaftLocation == null)
            {
                return NotFound();
            }
            return View(shaftLocation);
        }

        // POST: ShaftLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shaftLocation = await _context.ShaftLocations.FindAsync(id);
            _context.ShaftLocations.Remove(shaftLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShaftLocationExists(int id)
        {
            return _context.ShaftLocations.Any(e => e.ShaftId == id);
        }
    }
}
