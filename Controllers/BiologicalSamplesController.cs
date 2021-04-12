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
    public class BiologicalSamplesController : Controller
    {
        private readonly FEGBExcavationContext _context;

        public BiologicalSamplesController(FEGBExcavationContext context)
        {
            _context = context;
        }

        // GET: BiologicalSamples
        public async Task<IActionResult> Index()
        {
            var fEGBExcavationContext = _context.BiologicalSamples.Include(b => b.Mummy).Include(b => b.Shaft);
            return View(await fEGBExcavationContext.ToListAsync());
        }

        // GET: BiologicalSamples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biologicalSample = await _context.BiologicalSamples
                .Include(b => b.Mummy)
                .Include(b => b.Shaft)
                .FirstOrDefaultAsync(m => m.BioSampleId == id);
            if (biologicalSample == null)
            {
                return NotFound();
            }

            return View(biologicalSample);
        }

        // GET: BiologicalSamples/Create
        public IActionResult Create()
        {
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId");
            ViewData["ShaftId"] = new SelectList(_context.ShaftLocations, "ShaftId", "ShaftId");
            return View();
        }

        // POST: BiologicalSamples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BioSampleId,RackNum,BagNum,ShaftId,BurialNum,MummyId,ClusterNum,Notes,Initials,SampledMonth,SampledDay,SampledYear,PreviouslySampled")] BiologicalSample biologicalSample)
        {
            if (ModelState.IsValid)
            {
                _context.Add(biologicalSample);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", biologicalSample.MummyId);
            ViewData["ShaftId"] = new SelectList(_context.ShaftLocations, "ShaftId", "ShaftId", biologicalSample.ShaftId);
            return View(biologicalSample);
        }

        // GET: BiologicalSamples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biologicalSample = await _context.BiologicalSamples.FindAsync(id);
            if (biologicalSample == null)
            {
                return NotFound();
            }
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", biologicalSample.MummyId);
            ViewData["ShaftId"] = new SelectList(_context.ShaftLocations, "ShaftId", "ShaftId", biologicalSample.ShaftId);
            return View(biologicalSample);
        }

        // POST: BiologicalSamples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BioSampleId,RackNum,BagNum,ShaftId,BurialNum,MummyId,ClusterNum,Notes,Initials,SampledMonth,SampledDay,SampledYear,PreviouslySampled")] BiologicalSample biologicalSample)
        {
            if (id != biologicalSample.BioSampleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biologicalSample);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiologicalSampleExists(biologicalSample.BioSampleId))
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
            ViewData["MummyId"] = new SelectList(_context.Mummies, "MummyId", "MummyId", biologicalSample.MummyId);
            ViewData["ShaftId"] = new SelectList(_context.ShaftLocations, "ShaftId", "ShaftId", biologicalSample.ShaftId);
            return View(biologicalSample);
        }

        // GET: BiologicalSamples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biologicalSample = await _context.BiologicalSamples
                .Include(b => b.Mummy)
                .Include(b => b.Shaft)
                .FirstOrDefaultAsync(m => m.BioSampleId == id);
            if (biologicalSample == null)
            {
                return NotFound();
            }

            return View(biologicalSample);
        }

        // POST: BiologicalSamples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biologicalSample = await _context.BiologicalSamples.FindAsync(id);
            _context.BiologicalSamples.Remove(biologicalSample);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BiologicalSampleExists(int id)
        {
            return _context.BiologicalSamples.Any(e => e.BioSampleId == id);
        }
    }
}
