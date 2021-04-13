using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using WINTEX.DAL;
using WINTEX.Infrastructure;
using WINTEX.Models;
using WINTEX.Models.ViewModels;

namespace WINTEX
{
    public class MummiesController : Controller
    {
        private readonly ILogger<MummiesController> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        private readonly FEGBExcavationContext _context;

        public MummiesController(ILogger<MummiesController> logger, IDiagnosticContext diagnosticContext, FEGBExcavationContext context)
        {
            _logger = logger;
            _diagnosticContext = diagnosticContext;
            _context = context;
        }

        // GET: Mummies
        public IActionResult Index(int pageNum = 1)
        {
            ViewData["PresIndex"] = new SelectList(_context.Mummies.Select(m => m.PreservationIndex).Distinct(), ViewBag.PresIndex);
            ViewData["HairColorCode"] = new SelectList(_context.HairColorCodes.ToList(), "HairColorCode", "HairColorDescription", ViewBag.HairColorCode);
            ViewData["HeadDirection"] = new SelectList(_context.Mummies.Select(m => m.HeadDirection).Distinct(), ViewBag.HeadDirection);
            ViewData["MaxDepth"] = _context.Mummies.Max(m => m.BurialDepth);
            IQueryable<Mummy> list = _context.Mummies.Include(m => m.Shaft).Include(m => m.Tomb);
            if (TempData.ContainsKey("Filters") && (string)TempData["Filters"] == "true")
            {
                _logger.LogInformation("Filter is not null");
                if (TempData["hair-color"] != null && (string)TempData["hair-color"] != "all")
                {
                    list = list.Where(m => m.HairColorCode == (string)TempData["hair-color"]);
                }
                if ((string)TempData["pres-index"] != null && (string)TempData["pres-index"] != "all")
                {
                    list = list.Where(m => m.PreservationIndex == (string)TempData["pres-index"]);
                }
                if ((string)TempData["head"] != null && (string)TempData["head"] != "all")
                {
                    list = list.Where(m => m.HeadDirection == (string)TempData["head"]);
                }
                if ((string)TempData["burial-depth"] != null && (string)TempData["burial-depth"] != "all")
                {
                    list = list.Where(m => m.BurialDepth >= Convert.ToDecimal(TempData["burial-depth"]));
                }
                ViewData["FilterBurialDepth"] = TempData["burial-depth"];
            }
            int pageSize = 20;
            var pageInfo = new Paginator<Mummy>(pageSize, list);
            ViewBag.CurrentPage = pageNum;
            ViewBag.TotalPages = pageInfo.TotalPages;
            ViewBag.HasPreviousPage = !(pageNum > 1) ? "disabled" : "";
            ViewBag.HasNextPage = !(pageNum < pageInfo.TotalPages) ? "disabled" : "";
            return View(pageInfo.GetItems(pageNum));
        }

        [HttpPost]
        public IActionResult FilterMummies(IFormCollection pairs)
        {
            bool contains = false;
            foreach (string key in pairs.Keys)
            {
                if(pairs[key] != "" && !key.StartsWith("_"))
                {
                    TempData[key.ToString()] = pairs[key][0];
                    contains = true;
                }
            }
            if (contains)
            {
                TempData["Filters"] = "true";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearFilters()
        {
            TempData.Clear();
            return RedirectToAction("Index");
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
                .Include(m => m.MummyNotes)
                .Include(m => m.OsteologicalMummyDatum)
                .Include(m => m.PostExhumationDatum)
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
            _logger.LogInformation("{Protocol} {Method} {Path} by {User}", Request.Protocol, Request.Method, Request.Path, User.Identity.Name);
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
