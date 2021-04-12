using System;
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
    public class OsteologicalMummyDatumsController : Controller
    {
        private readonly FEGBExcavationContext _context;

        public OsteologicalMummyDatumsController(FEGBExcavationContext context)
        {
            _context = context;
        }

        // GET: OsteologicalMummyDatums
        public async Task<IActionResult> Index()
        {
            var fEGBExcavationContext = _context.OsteologicalMummyData.Include(o => o.Mummy);
            return View(await fEGBExcavationContext.ToListAsync());
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
