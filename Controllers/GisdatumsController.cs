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

namespace WINTEX
{
    public class GisdatumsController : Controller
    {
        private readonly FEGBExcavationContext _context;

        public GisdatumsController(FEGBExcavationContext context)
        {
            _context = context;
        }

        // GET: Gisdatums
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            var list = _context.Gisdata.Include(g => g.Mummy);
            var pageInfo = new Paginator<Gisdatum>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            return View(pageInfo.GetItems(pageNum));
        }

        // GET: Gisdatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gisdatum = await _context.Gisdata
                .Include(g => g.Mummy)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (gisdatum == null)
            {
                return NotFound();
            }

            return View(gisdatum);
        }

        // GET: Gisdatums/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId");
            return View();
        }

        // POST: Gisdatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("MummyId,MaturityCode,WrappingCode,Gisid")] Gisdatum gisdatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gisdatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", gisdatum.MummyId);
            return View(gisdatum);
        }

        // GET: Gisdatums/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gisdatum = await _context.Gisdata.FindAsync(id);
            if (gisdatum == null)
            {
                return NotFound();
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", gisdatum.MummyId);
            return View(gisdatum);
        }

        // POST: Gisdatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("MummyId,MaturityCode,WrappingCode,Gisid")] Gisdatum gisdatum)
        {
            if (id != gisdatum.MummyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gisdatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GisdatumExists(gisdatum.MummyId))
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
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", gisdatum.MummyId);
            return View(gisdatum);
        }

        // GET: Gisdatums/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gisdatum = await _context.Gisdata
                .Include(g => g.Mummy)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (gisdatum == null)
            {
                return NotFound();
            }

            return View(gisdatum);
        }

        // POST: Gisdatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gisdatum = await _context.Gisdata.FindAsync(id);
            _context.Gisdata.Remove(gisdatum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GisdatumExists(int id)
        {
            return _context.Gisdata.Any(e => e.MummyId == id);
        }
    }
}
