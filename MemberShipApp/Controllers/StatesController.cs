using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemberShipApp.Models;

using MemberShipApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace MemberShipApp.Controllers
{
    [Authorize]
    public class StatesController : Controller
    {
        private readonly IStateServices _stateService;
        private readonly IRegionServices _regionService;
        public StatesController(IStateServices stateService, IRegionServices regionService)
        {
            _regionService = regionService;
            _stateService = stateService;
        }

        // GET: States
        public async Task<IActionResult> Index()
        {
            var regions = await _regionService.GetAllRegions();
            ViewData["Regions"] = regions;
            var states = await _stateService.GetAllStates();
            return View(states);
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _stateService.GetStateById(id.Value);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: States/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RegionID"] = new SelectList(await _regionService.GetAllRegions(), "RegionID", "Name");
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateID,RegionID,Code,Name")] StateDto state)
        {
            if (ModelState.IsValid)
            {
                
                await _stateService.CreateState(state);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionID"] = new SelectList(await _regionService.GetAllRegions(), "RegionID", "Name", state.RegionID);
            return View(state);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _stateService.GetStateById(id.Value);
            if (state == null)
            {
                return NotFound();
            }
            ViewData["RegionID"] = new SelectList(await _regionService.GetAllRegions(), "RegionID", "Name", state.RegionID);
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateID,RegionID,Code,Name")] StateDto state)
        {
            if (id != state.StateID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _stateService.UpdateState(id, state);
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                        throw;
                    
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionID"] = new SelectList(await _regionService.GetAllRegions(), "RegionID", "Name", state.RegionID);
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var state = await _stateService.GetStateById(id.Value);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var state = await _stateService.DeleteState(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool StateExists(int id)
        //{
        //    return _context.States.Any(e => e.StateID == id);
        //}
    }
}
