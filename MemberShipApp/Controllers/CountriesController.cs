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
    public class CountriesController : Controller
    {
        private readonly ICountryServices _service;

        public CountriesController(ICountryServices service)
        {
            _service = service;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllCountries());
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _service.GetAllCountryWithConnect(id.Value);    //GetCountryById(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryID,Code,Name")] Country country)
        {
            if (ModelState.IsValid)
            {              
               var result = await _service.CreateCountry(country);
                if (result.IsSuccessful())
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessages[0]);
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _service.GetCountryById(id.Value);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryID,Code,Name")] Country country)
        {
            if (id != country.CountryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {                  
                   var result =  await _service.UpdateCountry(id, country);
                    if (result.IsSuccessful())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, result.ErrorMessages.Any()? result.ErrorMessages[0]:"An error occured");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    return View(country);
                }
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _service.GetCountryById(id.Value);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            try
            {
                var result = await _service.DeleteCountry(id);
                if (result.IsSuccessful())
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessages.Any() ? result.ErrorMessages[0] : "An error occured");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
                return View();
            };

            return RedirectToAction(nameof(Index));
        }

        //private bool CountryExists(int id)
        //{
        //    return _context.Countries.Any(e => e.CountryID == id);
        //}
    }
}
