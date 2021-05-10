using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberShipApp.Models;
using MemberShipApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MemberShipApp.Controllers
{
    public class DashBoardController : Controller
    {

        private readonly ICountryServices _countryService;
        private readonly IRegionServices _regionService;
        private readonly IStateServices _stateService;
        public DashBoardController(ICountryServices countryService, IRegionServices regionService, IStateServices stateService)
        {
            this._countryService = countryService;
            this._regionService = regionService;
            this._stateService = stateService;

        }
        public async Task<IActionResult> AssignStatesToRegion()
        {
            var countries = await _countryService.GetAllCountries();
            var id = countries.First().CountryID;
            var data = new StatesByRegionsView
            {
                regions = await _regionService.GetAllRegionByCountryID(id),
                CountryID = id
            };

            ViewData["CountryID"] = new SelectList(countries, "CountryID", "Name");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AssignStatesToRegion(int CountryID)
        {
            var countries = await _countryService.GetAllCountries();

            var data = new StatesByRegionsView
            {
                regions = await _regionService.GetAllRegionByCountryID(CountryID),
                CountryID = CountryID
            };

            ViewData["CountryID"] = new SelectList(countries, "CountryID", "Name", CountryID);
            return View(data);
        }

        public IActionResult Assign()
        {
            return View(new RegionDto { });
        }
    }
}