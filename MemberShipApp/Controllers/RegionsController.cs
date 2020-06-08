using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberShipApp.Models;
using UniversityApp.Data;
using MemberShipApp.Services;

namespace MemberShipApp.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IRegionServices _regionService;
        private readonly ICountryServices _countryService;
        private readonly IStateServices _stateService;
        public RegionsController(IRegionServices regionService, ICountryServices countryService, IStateServices stateService)
        {
            _regionService = regionService;
            _countryService = countryService;
            _stateService = stateService;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
            
            return View(await _regionService.GetAllRegions());
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _regionService.GetRegionWithStatesById(id.Value);
                //await _context.Regions
                //.Include(r => r.States)
                //.Include(w=>w.Country)
                //.FirstOrDefaultAsync(m => m.RegionID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Regions/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CountryID"] = new SelectList(await _countryService.GetAllCountries(), "CountryID", "Name");
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionID,CountryID,Name,Description")] RegionDto region)
        {
            if (ModelState.IsValid)
            {
                var result = await _regionService.CreateRegion(region);
                if (result.IsSuccessful())
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessages[0]);
            }
            ViewData["CountryID"] = new SelectList(await _countryService.GetAllCountries(), "CountryID", "Name", region.CountryID);
            return View(region);
        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await  _regionService.GetRegionWithStatesById(id.Value);
            if (region == null)
            {
                return NotFound();
            }

           
            var states = await _stateService.GetAllStatesByCountryID(region.CountryID);
          
            ViewBag.States = states.ToArray();
            ViewData["CountryID"] = new SelectList(await _countryService.GetAllCountries(), "CountryID", "Name", region.CountryID);
            return View(region);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  RegionDto region)
        {
            if (id != region.RegionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _regionService.UpdateRegion(id, region);
                    if (result.IsSuccessful())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, result.ErrorMessages.Any() ? result.ErrorMessages[0] : "An error occured");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(await _countryService.GetAllCountries(), "CountryID", "Name", region.CountryID);
            return View(region);
        }

        // GET: Regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _regionService.GetRegionById(id.Value);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var region = await _regionService.DeleteRegion(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
