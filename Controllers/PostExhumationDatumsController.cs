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
    public class PostExhumationDatumsController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public PostExhumationDatumsController(IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            this.diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: PostExhumationDatums
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            var list = _context.PostExhumationData.Include(p => p.Mummy);
            var pageInfo = new Paginator<PostExhumationDatum>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            return View(pageInfo.GetItems(pageNum));
        }

        // GET: PostExhumationDatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postExhumationDatum = await _context.PostExhumationData
                .Include(p => p.Mummy)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (postExhumationDatum == null)
            {
                return NotFound();
            }

            return View(postExhumationDatum);
        }

        // GET: PostExhumationDatums/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId");
            return View();
        }

        // POST: PostExhumationDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("MummyId,HairTaken,SoftTissueTaken,BoneTaken,ToothTaken,TextileTaken,BurialSampleTaken,DescriptionOfTaken,SampleNum,Sex,SexBodyCol,GefunctionTotal,PreservationNotes,AgeAtDeath,EstimateLivingStature,BodyAnalysis,SexBurialMethod,FaceBundle")] PostExhumationDatum postExhumationDatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postExhumationDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", postExhumationDatum.MummyId);
            return View(postExhumationDatum);
        }

        // GET: PostExhumationDatums/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postExhumationDatum = await _context.PostExhumationData.FindAsync(id);
            if (postExhumationDatum == null)
            {
                return NotFound();
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", postExhumationDatum.MummyId);
            return View(postExhumationDatum);
        }

        // POST: PostExhumationDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("MummyId,HairTaken,SoftTissueTaken,BoneTaken,ToothTaken,TextileTaken,BurialSampleTaken,DescriptionOfTaken,SampleNum,Sex,SexBodyCol,GefunctionTotal,PreservationNotes,AgeAtDeath,EstimateLivingStature,BodyAnalysis,SexBurialMethod,FaceBundle")] PostExhumationDatum postExhumationDatum)
        {
            if (id != postExhumationDatum.MummyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postExhumationDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExhumationDatumExists(postExhumationDatum.MummyId))
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
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", postExhumationDatum.MummyId);
            return View(postExhumationDatum);
        }

        // GET: PostExhumationDatums/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postExhumationDatum = await _context.PostExhumationData
                .Include(p => p.Mummy)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (postExhumationDatum == null)
            {
                return NotFound();
            }

            return View(postExhumationDatum);
        }

        // POST: PostExhumationDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postExhumationDatum = await _context.PostExhumationData.FindAsync(id);
            _context.PostExhumationData.Remove(postExhumationDatum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExhumationDatumExists(int id)
        {
            return _context.PostExhumationData.Any(e => e.MummyId == id);
        }
    }
}
