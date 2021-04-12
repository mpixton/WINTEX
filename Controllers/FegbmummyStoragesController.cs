﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WINTEX.DAL;
using WINTEX.Models;

namespace WINTEX
{
    public class FegbmummyStoragesController : Controller
    {
        private readonly FEGBExcavationContext _context;

        public FegbmummyStoragesController(FEGBExcavationContext context)
        {
            _context = context;
        }

        // GET: FegbmummyStorages
        public async Task<IActionResult> Index()
        {
            var fEGBExcavationContext = _context.FegbmummyStorages.Include(f => f.Mummy).Include(f => f.Shelf);
            return View(await fEGBExcavationContext.ToListAsync());
        }

        // GET: FegbmummyStorages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbmummyStorage = await _context.FegbmummyStorages
                .Include(f => f.Mummy)
                .Include(f => f.Shelf)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (fegbmummyStorage == null)
            {
                return NotFound();
            }

            return View(fegbmummyStorage);
        }

        // GET: FegbmummyStorages/Create
        public IActionResult Create()
        {
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId");
            ViewData["ShelfId"] = new SelectList(_context.FegbstorageLocations, "ShelfId", "Shelf");
            return View();
        }

        // POST: FegbmummyStorages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MummyId,ShelfId")] FegbmummyStorage fegbmummyStorage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fegbmummyStorage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", fegbmummyStorage.MummyId);
            ViewData["ShelfId"] = new SelectList(_context.FegbstorageLocations, "ShelfId", "Shelf", fegbmummyStorage.ShelfId);
            return View(fegbmummyStorage);
        }

        // GET: FegbmummyStorages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbmummyStorage = await _context.FegbmummyStorages.FindAsync(id);
            if (fegbmummyStorage == null)
            {
                return NotFound();
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", fegbmummyStorage.MummyId);
            ViewData["ShelfId"] = new SelectList(_context.FegbstorageLocations, "ShelfId", "Shelf", fegbmummyStorage.ShelfId);
            return View(fegbmummyStorage);
        }

        // POST: FegbmummyStorages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MummyId,ShelfId")] FegbmummyStorage fegbmummyStorage)
        {
            if (id != fegbmummyStorage.MummyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fegbmummyStorage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FegbmummyStorageExists(fegbmummyStorage.MummyId))
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
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", fegbmummyStorage.MummyId);
            ViewData["ShelfId"] = new SelectList(_context.FegbstorageLocations, "ShelfId", "Shelf", fegbmummyStorage.ShelfId);
            return View(fegbmummyStorage);
        }

        // GET: FegbmummyStorages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbmummyStorage = await _context.FegbmummyStorages
                .Include(f => f.Mummy)
                .Include(f => f.Shelf)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (fegbmummyStorage == null)
            {
                return NotFound();
            }

            return View(fegbmummyStorage);
        }

        // POST: FegbmummyStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fegbmummyStorage = await _context.FegbmummyStorages.FindAsync(id);
            _context.FegbmummyStorages.Remove(fegbmummyStorage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FegbmummyStorageExists(int id)
        {
            return _context.FegbmummyStorages.Any(e => e.MummyId == id);
        }
    }
}
