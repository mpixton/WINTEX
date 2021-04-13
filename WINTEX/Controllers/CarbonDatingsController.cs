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

namespace WINTEX
{
    public class CarbonDatingsController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public CarbonDatingsController(IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            this.diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: CarbonDatings
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            var list = _context.CarbonDatings.Include(c => c.Mummy).Include(c => c.ShaftLocation);
            var pageInfo = new Paginator<CarbonDating>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            return View(pageInfo.GetItems(pageNum));
        }

        // GET: CarbonDatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carbonDating = await _context.CarbonDatings
                .Include(c => c.Mummy)
                .Include(c => c.ShaftLocation)
                .FirstOrDefaultAsync(m => m.CarbonDatingId == id);
            if (carbonDating == null)
            {
                return NotFound();
            }

            return View(carbonDating);
        }

        // GET: CarbonDatings/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId");
            ViewData["ShaftLocationId"] = new SelectList(_context.ShaftLocations, "ShaftId", "ShaftId");
            return View();
        }

        // POST: CarbonDatings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("CarbonDatingId,Racknum,ShaftLocationId,BurialNum,MummyId,AreaHillBurialNum,TubeNum,Description,SizeMm,Foci,C14sample2017,LocationDescription,Questions,Conventional14CageBp,_14ccalendarDate,Calibrated95PerCalendarDateMax,Calibrated95PerCalendarDateMin,Calibrated95PerCalendarDateSpan,Calibrated95perCalendarDateAvg,Category,Notes")] CarbonDating carbonDating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carbonDating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", carbonDating.MummyId);
            ViewData["ShaftLocationId"] = new SelectList(_context.ShaftLocations, "ShaftId", "ShaftId", carbonDating.ShaftLocationId);
            return View(carbonDating);
        }

        // GET: CarbonDatings/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carbonDating = await _context.CarbonDatings.FindAsync(id);
            if (carbonDating == null)
            {
                return NotFound();
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", carbonDating.MummyId);
            ViewData["ShaftLocationId"] = new SelectList(_context.ShaftLocations, "ShaftId", "ShaftId", carbonDating.ShaftLocationId);
            return View(carbonDating);
        }

        // POST: CarbonDatings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int id, [Bind("CarbonDatingId,Racknum,ShaftLocationId,BurialNum,MummyId,AreaHillBurialNum,TubeNum,Description,SizeMm,Foci,C14sample2017,LocationDescription,Questions,Conventional14CageBp,_14ccalendarDate,Calibrated95PerCalendarDateMax,Calibrated95PerCalendarDateMin,Calibrated95PerCalendarDateSpan,Calibrated95perCalendarDateAvg,Category,Notes")] CarbonDating carbonDating)
        {
            if (id != carbonDating.CarbonDatingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carbonDating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarbonDatingExists(carbonDating.CarbonDatingId))
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
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", carbonDating.MummyId);
            ViewData["ShaftLocationId"] = new SelectList(_context.ShaftLocations, "ShaftId", "ShaftId", carbonDating.ShaftLocationId);
            return View(carbonDating);
        }

        // GET: CarbonDatings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carbonDating = await _context.CarbonDatings
                .Include(c => c.Mummy)
                .Include(c => c.ShaftLocation)
                .FirstOrDefaultAsync(m => m.CarbonDatingId == id);
            if (carbonDating == null)
            {
                return NotFound();
            }

            return View(carbonDating);
        }

        // POST: CarbonDatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carbonDating = await _context.CarbonDatings.FindAsync(id);
            _context.CarbonDatings.Remove(carbonDating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarbonDatingExists(int id)
        {
            return _context.CarbonDatings.Any(e => e.CarbonDatingId == id);
        }
    }
}
