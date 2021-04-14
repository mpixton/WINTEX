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
    public class MummyNotesController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public MummyNotesController(IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            this.diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: MummyNotes
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            var list = _context.MummyNotes.Include(m => m.Mummy);
            var pageInfo = new Paginator<MummyNote>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            ViewBag.HasPreviousPage = !(pageNum > 1) ? "disabled" : "";
            ViewBag.HasNextPage = !(pageNum < pageInfo.TotalPages) ? "disabled" : "";
            return View(pageInfo.GetItems(pageNum));
        }

        // GET: MummyNotes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mummyNote = await _context.MummyNotes
                .Include(m => m.Mummy)
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (mummyNote == null)
            {
                return NotFound();
            }

            return View(mummyNote);
        }

        // GET: MummyNotes/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId");
            return View();
        }

        // POST: MummyNotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("NoteId,MummyId,NoteType,NoteBody")] MummyNote mummyNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mummyNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", mummyNote.MummyId);
            return View(mummyNote);
        }

        // GET: MummyNotes/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mummyNote = await _context.MummyNotes.FindAsync(id);
            if (mummyNote == null)
            {
                return NotFound();
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", mummyNote.MummyId);
            return View(mummyNote);
        }

        // POST: MummyNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(long id, [Bind("NoteId,MummyId,NoteType,NoteBody")] MummyNote mummyNote)
        {
            if (id != mummyNote.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mummyNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MummyNoteExists(mummyNote.NoteId))
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
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", mummyNote.MummyId);
            return View(mummyNote);
        }

        // GET: MummyNotes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mummyNote = await _context.MummyNotes
                .Include(m => m.Mummy)
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (mummyNote == null)
            {
                return NotFound();
            }

            return View(mummyNote);
        }

        // POST: MummyNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var mummyNote = await _context.MummyNotes.FindAsync(id);
            _context.MummyNotes.Remove(mummyNote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MummyNoteExists(long id)
        {
            return _context.MummyNotes.Any(e => e.NoteId == id);
        }
    }
}
