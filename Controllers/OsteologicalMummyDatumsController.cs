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
    public class OsteologicalMummyDatumsController : Controller
    {
        private readonly IDiagnosticContext diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public OsteologicalMummyDatumsController(IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            this.diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: OsteologicalMummyDatums
        public IActionResult Index(int pageNum = 1)
        {
            int pageSize = 20;
            var list = _context.OsteologicalMummyData.Include(o => o.Mummy);
            var pageInfo = new Paginator<OsteologicalMummyDatum>(pageSize, list);
            ViewData["CurrentPage"] = pageNum;
            ViewData["TotalPages"] = pageInfo.TotalPages;
            ViewBag.HasPreviousPage = !(pageNum > 1) ? "disabled" : "";
            ViewBag.HasNextPage = !(pageNum < pageInfo.TotalPages) ? "disabled" : "";
            return View(pageInfo.GetItems(pageNum));
        }

        // GET: OsteologicalMummyDatums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osteologicalMummyDatum = await _context.OsteologicalMummyData
                .Include(o => o.Mummy)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (osteologicalMummyDatum == null)
            {
                return NotFound();
            }

            return View(osteologicalMummyDatum);
        }

        // GET: OsteologicalMummyDatums/Create
        [Authorize(Roles = "Researcher, Admin")]
        public IActionResult Create()
        {
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId");
            return View();
        }

        // POST: OsteologicalMummyDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher, Admin")]
        public async Task<IActionResult> Create([Bind("MummyId,FemurHead,HumerusHead,Osteophytosis,PubicSymphysis,FemurLength,HumerusLength,TibiaLength,Robust,SupraorbitalRidges,OrbitEdge,ParietalBossing,Gonian,NuchalCrest,ZygomaticCrest,CranialSuture,MaximumCranialLength,MaximumCranialBreadth,BasionBregmaHeight,BasionNasion,BasionProstionLength,BizygomaticDiameter,NasionProsthion,MaximumNasalBreadth,BasilarSuture,VentralArc,SubpubicAngle,SciaticNotch,PubicBone,PreaurSulcus,MedialIpramus,DorsalPitting,InterorbitalBreadth,BurialHairColor,ToothAttrition,ToothEruption,PathologyAnomalies,EphiphysealUnion,SkullTrauma,PostcraniaTrauma,CribraOrbitala,PoroticHyperostosis,MetopicSuture,ButtonOsteoma,TemporalMandibularJointOsteoarthritis,LinearHypoplasiaEnamel,PoroticHyperostosisLocations,OsteologyUnknownComment,ToBeConfirmed")] OsteologicalMummyDatum osteologicalMummyDatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(osteologicalMummyDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", osteologicalMummyDatum.MummyId);
            return View(osteologicalMummyDatum);
        }

        // GET: OsteologicalMummyDatums/Edit/5
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osteologicalMummyDatum = await _context.OsteologicalMummyData.FindAsync(id);
            if (osteologicalMummyDatum == null)
            {
                return NotFound();
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", osteologicalMummyDatum.MummyId);
            return View(osteologicalMummyDatum);
        }

        // POST: OsteologicalMummyDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Researcher")]
        public async Task<IActionResult> Edit(int id, [Bind("MummyId,FemurHead,HumerusHead,Osteophytosis,PubicSymphysis,FemurLength,HumerusLength,TibiaLength,Robust,SupraorbitalRidges,OrbitEdge,ParietalBossing,Gonian,NuchalCrest,ZygomaticCrest,CranialSuture,MaximumCranialLength,MaximumCranialBreadth,BasionBregmaHeight,BasionNasion,BasionProstionLength,BizygomaticDiameter,NasionProsthion,MaximumNasalBreadth,BasilarSuture,VentralArc,SubpubicAngle,SciaticNotch,PubicBone,PreaurSulcus,MedialIpramus,DorsalPitting,InterorbitalBreadth,BurialHairColor,ToothAttrition,ToothEruption,PathologyAnomalies,EphiphysealUnion,SkullTrauma,PostcraniaTrauma,CribraOrbitala,PoroticHyperostosis,MetopicSuture,ButtonOsteoma,TemporalMandibularJointOsteoarthritis,LinearHypoplasiaEnamel,PoroticHyperostosisLocations,OsteologyUnknownComment,ToBeConfirmed")] OsteologicalMummyDatum osteologicalMummyDatum)
        {
            if (id != osteologicalMummyDatum.MummyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(osteologicalMummyDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsteologicalMummyDatumExists(osteologicalMummyDatum.MummyId))
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
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", osteologicalMummyDatum.MummyId);
            return View(osteologicalMummyDatum);
        }

        // GET: OsteologicalMummyDatums/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var osteologicalMummyDatum = await _context.OsteologicalMummyData
                .Include(o => o.Mummy)
                .FirstOrDefaultAsync(m => m.MummyId == id);
            if (osteologicalMummyDatum == null)
            {
                return NotFound();
            }

            return View(osteologicalMummyDatum);
        }

        // POST: OsteologicalMummyDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var osteologicalMummyDatum = await _context.OsteologicalMummyData.FindAsync(id);
            _context.OsteologicalMummyData.Remove(osteologicalMummyDatum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OsteologicalMummyDatumExists(int id)
        {
            return _context.OsteologicalMummyData.Any(e => e.MummyId == id);
        }
    }
}
