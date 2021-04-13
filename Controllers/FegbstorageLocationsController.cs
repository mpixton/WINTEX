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
    public class FegbstorageLocationsController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public FegbstorageLocationsController(IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            this.diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: FegbstorageLocations
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            var list = _context.FegbstorageLocations;
            var pageInfo = new Paginator<FegbstorageLocation>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            return View(pageInfo.GetItems(pageNum));
        }

        // GET: FegbstorageLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbstorageLocation = await _context.FegbstorageLocations
                .FirstOrDefaultAsync(m => m.ShelfId == id);
            if (fegbstorageLocation == null)
            {
                return NotFound();
            }

            return View(fegbstorageLocation);
        }

        // GET: FegbstorageLocations/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FegbstorageLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("ShelfId,Rack,Shelf,SubShelf")] FegbstorageLocation fegbstorageLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fegbstorageLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fegbstorageLocation);
        }

        // GET: FegbstorageLocations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbstorageLocation = await _context.FegbstorageLocations.FindAsync(id);
            if (fegbstorageLocation == null)
            {
                return NotFound();
            }
            return View(fegbstorageLocation);
        }

        // POST: FegbstorageLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ShelfId,Rack,Shelf,SubShelf")] FegbstorageLocation fegbstorageLocation)
        {
            if (id != fegbstorageLocation.ShelfId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fegbstorageLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FegbstorageLocationExists(fegbstorageLocation.ShelfId))
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
            return View(fegbstorageLocation);
        }

        // GET: FegbstorageLocations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fegbstorageLocation = await _context.FegbstorageLocations
                .FirstOrDefaultAsync(m => m.ShelfId == id);
            if (fegbstorageLocation == null)
            {
                return NotFound();
            }

            return View(fegbstorageLocation);
        }

        // POST: FegbstorageLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fegbstorageLocation = await _context.FegbstorageLocations.FindAsync(id);
            _context.FegbstorageLocations.Remove(fegbstorageLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FegbstorageLocationExists(int id)
        {
            return _context.FegbstorageLocations.Any(e => e.ShelfId == id);
        }
    }
}
