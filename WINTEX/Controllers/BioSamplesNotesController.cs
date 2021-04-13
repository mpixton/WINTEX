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
    public class BioSamplesNotesController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public BioSamplesNotesController(IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            this.diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: BioSamplesNotes
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            
            var list = _context.BioSamplesNotes.Include(b => b.BioSample);
            var pageInfo = new Paginator<BioSamplesNote>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            return View(pageInfo.GetItems(pageNum));
        }

        // GET: BioSamplesNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bioSamplesNote = await _context.BioSamplesNotes
                .Include(b => b.BioSample)
                .FirstOrDefaultAsync(m => m.BioNoteId == id);
            if (bioSamplesNote == null)
            {
                return NotFound();
            }

            return View(bioSamplesNote);
        }

        // GET: BioSamplesNotes/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            ViewData["BioSampleId"] = new SelectList(_context.BiologicalSamples, "BioSampleId", "BioSampleId");
            return View();
        }

        // POST: BioSamplesNotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("BioNoteId,BioSampleId,NoteBody")] BioSamplesNote bioSamplesNote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bioSamplesNote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BioSampleId"] = new SelectList(_context.BiologicalSamples, "BioSampleId", "BioSampleId", bioSamplesNote.BioSampleId);
            return View(bioSamplesNote);
        }

        // GET: BioSamplesNotes/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bioSamplesNote = await _context.BioSamplesNotes.FindAsync(id);
            if (bioSamplesNote == null)
            {
                return NotFound();
            }
            ViewData["BioSampleId"] = new SelectList(_context.BiologicalSamples, "BioSampleId", "BioSampleId", bioSamplesNote.BioSampleId);
            return View(bioSamplesNote);
        }

        // POST: BioSamplesNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int id, [Bind("BioNoteId,BioSampleId,NoteBody")] BioSamplesNote bioSamplesNote)
        {
            if (id != bioSamplesNote.BioNoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bioSamplesNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BioSamplesNoteExists(bioSamplesNote.BioNoteId))
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
            ViewData["BioSampleId"] = new SelectList(_context.BiologicalSamples, "BioSampleId", "BioSampleId", bioSamplesNote.BioSampleId);
            return View(bioSamplesNote);
        }

        // GET: BioSamplesNotes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bioSamplesNote = await _context.BioSamplesNotes
                .Include(b => b.BioSample)
                .FirstOrDefaultAsync(m => m.BioNoteId == id);
            if (bioSamplesNote == null)
            {
                return NotFound();
            }

            return View(bioSamplesNote);
        }

        // POST: BioSamplesNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bioSamplesNote = await _context.BioSamplesNotes.FindAsync(id);
            _context.BioSamplesNotes.Remove(bioSamplesNote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BioSamplesNoteExists(int id)
        {
            return _context.BioSamplesNotes.Any(e => e.BioNoteId == id);
        }
    }
}
