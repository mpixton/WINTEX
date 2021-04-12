using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WINTEX.DAL;
using WINTEX.Infrastructure;
using WINTEX.Models;
using WINTEX.Models.ViewModels;

namespace WINTEX
{
    public class MummiesController : Controller
    {
        private readonly FEGBExcavationContext _context;

        public MummiesController(FEGBExcavationContext context)
        {
            _context = context;
        }

        // GET: Mummies
        public async Task<IActionResult> Index(int currPage = 1)
        {
            var fEGBExcavationContext = _context.Mummies.Include(m => m.Shaft).Include(m => m.Tomb);
            var pageInfo = new Paginator<Mummy>(20, fEGBExcavationContext);
            ViewBag.CurrentPage = currPage;
            ViewBag.TotalPages = pageInfo.TotalPages;
            return View(pageInfo.GetItems(currPage));
        }

        // GET: Mummies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mummy = await _context.Mummies
                .Include(m => m.Shaft)
                .Include(m => m.Tomb)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (mummy == null)
            {
                return NotFound();
            }

            return View(mummy);
        }

        // GET: Mummies/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            ViewData["ShaftId"] = new SelectList(_context.ShaftLocations.Where(s => s.Yupper != null && s.Xupper != null), "ShaftId", "Lookup");
            ViewData["TombId"] = new SelectList(_context.TombLocations, "TombLocationId", "LookupValue");
            return View();
        }

        // POST: Mummies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("MummyId,BurialNum,ShaftId,TombId,BurialDepth,WestToHead,WestToFeet,SouthToHead,SouthToFeet,Length,BurialSituation,Goods,ArtifactsDescription,Photo,PreservationIndex,ClusterNum,HairColorCode,AgeCodeSingle,BurialMaterials,ExcavationRecorder,DateExcavated,HeadDirection,ArtifactFound")] Mummy mummy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mummy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShaftId"] = new SelectList(_context.ShaftLocations.Where(s => s.Yupper != null && s.Xupper != null), "ShaftId", "Lookup", mummy.ShaftId);
            ViewData["TombId"] = new SelectList(_context.TombLocations, "TombLocationId", "LookupValue",mummy.TombId);
            return View(mummy);
        }

        // GET: Mummies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mummy = await _context.Mummies.FindAsync(id);
            if (mummy == null)
            {
                return NotFound();
            }
            ViewData["ShaftId"] = new SelectList(_context.ShaftLocations.Where(s => s.Yupper != null && s.Xupper != null), "ShaftId", "Lookup", mummy.ShaftId);
            ViewData["TombId"] = new SelectList(_context.TombLocations, "TombLocationId", "LookupValue", mummy.TombId);
            return View(mummy);
        }

        // POST: Mummies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("MummyId,BurialNum,ShaftId,TombId,BurialDepth,WestToHead,WestToFeet,SouthToHead,SouthToFeet,Length,BurialSituation,Goods,ArtifactsDescription,Photo,PreservationIndex,ClusterNum,HairColorCode,AgeCodeSingle,BurialMaterials,ExcavationRecorder,DateExcavated,YearExcavated,MonthExcavated,DayExcavated,HeadDirection,ArtifactFound")] Mummy mummy)
        {
            if (id != mummy.MummyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mummy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MummyExists(mummy.MummyId))
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
            ViewData["ShaftId"] = new SelectList(_context.ShaftLocations.Where(s => s.Yupper != null && s.Xupper != null), "ShaftId", "Lookup", mummy.ShaftId);
            ViewData["TombId"] = new SelectList(_context.TombLocations, "TombLocationId", "LookupValue", mummy.TombId);
            return View(mummy);
        }

        // GET: Mummies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mummy = await _context.Mummies
                .Include(m => m.Shaft)
                .Include(m => m.Tomb)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (mummy == null)
            {
                return NotFound();
            }

            return View(mummy);
        }

        // POST: Mummies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mummy = await _context.Mummies.FindAsync(id);
            _context.Mummies.Remove(mummy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MummyExists(int id)
        {
            return _context.Mummies.Any(e => e.MummyId == id);
        }
    }
}
