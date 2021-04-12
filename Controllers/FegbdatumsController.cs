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

namespace WINTEX
{
    public class FegbdatumsController : Controller
    {
        private readonly FEGBExcavationContext _context;

        public FegbdatumsController(FEGBExcavationContext context)
        {
            _context = context;
        }

        // GET: Fegbdatums
        public async Task<IActionResult> Index()
        {
            var fEGBExcavationContext = _context.Fegbdata.Include(f => f.Mummy);
            return View(await fEGBExcavationContext.ToListAsync());
        }

        // GET: Fegbdatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbdatum = await _context.Fegbdata
                .Include(f => f.Mummy)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (fegbdatum == null)
            {
                return NotFound();
            }

            return View(fegbdatum);
        }

        // GET: Fegbdatums/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId");
            return View();
        }

        // POST: Fegbdatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("MummyId,YearOnSkull,MonthOnSkull,DayOnSkull,FieldBook,FieldBookPageNum,PostcraniaAtMagazine,Byusample,SkullAtMagazine,Skull2018StudySex,Skull2018StudyAge")] Fegbdatum fegbdatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fegbdatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", fegbdatum.MummyId);
            return View(fegbdatum);
        }

        // GET: Fegbdatums/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbdatum = await _context.Fegbdata.FindAsync(id);
            if (fegbdatum == null)
            {
                return NotFound();
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", fegbdatum.MummyId);
            return View(fegbdatum);
        }

        // POST: Fegbdatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("MummyId,YearOnSkull,MonthOnSkull,DayOnSkull,FieldBook,FieldBookPageNum,PostcraniaAtMagazine,Byusample,SkullAtMagazine,Skull2018StudySex,Skull2018StudyAge")] Fegbdatum fegbdatum)
        {
            if (id != fegbdatum.MummyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fegbdatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FegbdatumExists(fegbdatum.MummyId))
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
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", fegbdatum.MummyId);
            return View(fegbdatum);
        }

        // GET: Fegbdatums/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbdatum = await _context.Fegbdata
                .Include(f => f.Mummy)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (fegbdatum == null)
            {
                return NotFound();
            }

            return View(fegbdatum);
        }

        // POST: Fegbdatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fegbdatum = await _context.Fegbdata.FindAsync(id);
            _context.Fegbdata.Remove(fegbdatum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FegbdatumExists(int id)
        {
            return _context.Fegbdata.Any(e => e.MummyId == id);
        }
    }
}
