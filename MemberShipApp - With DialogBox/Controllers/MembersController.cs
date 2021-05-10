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
using MemberShipApp.Extensions;
using MemberShipApp.Models.DTO;

namespace MemberShipApp.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberServices _memberService;
        private readonly IPositionServices _positionService;
        private readonly IStateServices _stateService;
        private readonly ICountryServices _countryService;
        private readonly IRegionServices _regionService;
        private readonly IContributionServices _contributionService;
        public MembersController
            (
            IMemberServices memberService, 
            IPositionServices positionService, 
            IStateServices stateService,
            ICountryServices countryService,
            IRegionServices regionService,
            IContributionServices contributionService
            )
        {
            this._memberService = memberService;
            this._positionService = positionService;
            this._stateService = stateService;
            this._countryService = countryService;
            this._regionService = regionService;
            this._contributionService = contributionService;
        }

        // GET: Members
        public async Task<IActionResult> Index(int? year, int? countryID, int? stateID)
        {
            int regionID = 0;
            var countries = await _countryService.GetAllCountryWithConnect();
            if (stateID.HasValue)
            {
                var region = countries.SelectMany(s => s.Regions).FirstOrDefault(s => s.States.Any(f => f.ID == stateID.Value));
                regionID = region.RegionID;
                countryID = countries.FirstOrDefault(s => s.Regions.Any(c => c.RegionID == regionID)).CountryID;
            }

            int newCountryID = countryID.HasValue ? countryID.Value : countries.First().CountryID;
            regionID = regionID==0 ? countries.Where(b=>b.CountryID== newCountryID).SelectMany(s => s.Regions).FirstOrDefault().RegionID: regionID;
           
            int newYear = year.HasValue ? year.Value : DateTime.Now.Year;
            var members = await _memberService.GetMemberYearlyContributionByCountryAsync(newCountryID, newYear);
            ViewData["Years"] = new SelectList(GenerateYears.PopulateYears(), "", "", newYear);
           
            var data = new MemberContributionView
            {
                Countries = countries.ToList(),
                Members = members.ToList(),
                SelectedCountryID = newCountryID,
                SelectedRegionID = regionID,
                SelectedStateID = stateID.HasValue ? stateID.Value : 0
            };

            return View(data);
        }

        //public async Task<IActionResult> Index(
        //string sortOrder,
        //string currentFilter,
        //string searchString,
        //int? pageNumber
        //)
        //{
        //    ViewData["CurrentSort"] = sortOrder;
        //    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewData["StateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "state_desc" : "";

        //    if (searchString != null)
        //    {
        //        pageNumber = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewData["CurrentFilter"] = searchString;

        //    var members = await _memberService.GetAllConnectMembers();
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        members =  members.Where(s => s.LastName.Contains(searchString)
        //                               || s.FirstName.Contains(searchString));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            members = members.OrderByDescending(s => s.LastName);
        //            break;
        //        //case "Date":
        //        //    members = members.OrderBy(s => s.StateID);
        //        //    break;
        //        case "state_desc":
        //            members = members.OrderByDescending(s => s.StateID);
        //            break;
        //        default:
        //            members = members.OrderBy(s => s.LastName);
        //            break;
        //    }

        //    int pageSize = 6;

        //    var data = new MemberView
        //    {
        //        PageList = PaginatedList<MemberDto>.CreateAsync(members, pageNumber ?? 1, pageSize),
        //        SelectedMember = null
        //    };

        //    return View(data);
        //}

 
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var member = await _memberService.GetMemberDetails(id.Value);   //.GetMemberById(id.Value);
                if (member == null)
                {
                    return NotFound();
                }
                return PartialView("_PartialDetails", member);
            }
            catch (Exception exp)
            {
                return PartialView("Error", new ErrorViewModel { RequestId= exp.Message.ToString() });
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try 
            { 
                if (id == null)
                {
                    return NotFound();
                }

                var member = await _memberService.GetMemberById(id.Value);
                if (member == null)
                {
                    return NotFound();
                }

                await ViewBagCache(member.StateID, member.PositionID);
                return PartialView("_PartialEdit", member);
            }
            catch (Exception exp)
            {
                return PartialView("Error", new ErrorViewModel { RequestId = exp.Message.ToString() });
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                await ViewBagCache(0, 0);
                var countries = await _countryService.GetAllCountryWithConnect();
                ViewData["Countries"] = countries.ToList();
                return PartialView("_PartialCreate");          
             }
            catch (Exception exp)
            {
                return PartialView("Error", new ErrorViewModel { RequestId= exp.Message.ToString()});
            }
        }


        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,PositionID,StateID,Code,FirstName,LastName,Phone,Email,StartDate,EndDate")] MemberDto member)
        {
            if (ModelState.IsValid)
            {
                try 
                { 
                    var result = await _memberService.CreateMember(member);
                    if (result.IsSuccessful())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, result.ErrorMessages[0]);
                    member.ErrorMessage = result.ErrorMessages.Any() ? result.ErrorMessages[0] : "An error occured";
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    member.ErrorMessage = ex.Message.ToString();
                }
            }
            await ViewBagCache(member.StateID, member.PositionID);

            member.ErrorMessage = ModelState.GetErrorsAsBRList().ToString();
            var members = await _memberService.GetAllConnectMembers();

            var data = new MemberView
            {
                PageList = PaginatedList<MemberDto>.CreateAsync(members, 1, 6),
                SelectedMember = member
            };
            return View("Index", data);
        }


        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberID,PositionID,StateID,Code,FirstName,LastName,Phone,Email,StartDate,EndDate")] MemberDto member)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _memberService.UpdateMember(member.MemberID, member);
                    if (result.IsSuccessful())
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError(string.Empty, result.ErrorMessages.Any() ? result.ErrorMessages[0] : "An error occured");
                    member.ErrorMessage = result.ErrorMessages.Any() ? result.ErrorMessages[0] : "An error occured";
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    member.ErrorMessage = ex.Message.ToString();
                }
               // return RedirectToAction(nameof(Index));
            }

            await ViewBagCache(member.StateID, member.PositionID);
            member.ErrorMessage = ModelState.GetErrorsAsBRList().ToString();
            var members =await _memberService.GetAllConnectMembers();

            var data = new MemberView
            {
                PageList = PaginatedList<MemberDto>.CreateAsync(members, 1, 6),
                SelectedMember = member
            };
            return View("Index",data);
            //return PartialView("_PartialEdit", member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = await _memberService.GetMemberById(id.Value);                                
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _memberService.DeleteMember(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddContribution(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var member = await _memberService.GetMemberById(id.Value);
                if (member == null)
                {
                    return NotFound();
                }
                ViewData["ContributionMethodID"] = new SelectList(await _contributionService.GetContributionMethod(), "ContributionMethodID", "Name");
                ViewData["Member"] = member;
                return PartialView("_PartialContribution");
            }
            catch (Exception exp)
            {
                return PartialView("Error", new ErrorViewModel { RequestId = exp.Message.ToString() });
            }
        }

        // POST: Contributions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContribution([Bind("ContributionID,MemberID,StateID,ContributionMethodID,Amount,ContributionDate")] ContributionDto contribution)
        {
            string error="";
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _contributionService.CreateContribution(contribution);
                    if (result.IsSuccessful())
                    {             
                        return RedirectToAction(nameof(Index), new {stateID=contribution.StateID });
                    }
                    ModelState.AddModelError(string.Empty, result.ErrorMessages[0]);
                    error = result.ErrorMessages.Any() ? result.ErrorMessages[0] : "An error occured";

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    error = ex.Message.ToString();
                }
            }
            else
            {
                error = ModelState.GetErrorsAsBRList().ToString();
            }
            
            var countries = await _countryService.GetAllCountryWithConnect();
            var region = countries.SelectMany(s => s.Regions).FirstOrDefault(s=>s.States.Any(f=>f.ID==contribution.StateID));
            var regionID = region.RegionID;
            var countryID = countries.FirstOrDefault(s => s.Regions.Any(c => c.RegionID == regionID)).CountryID;
            int newCountryID = countryID!=0 ? countryID : countries.First().CountryID;
            int newYear = DateTime.Now.Year;
            var members = await _memberService.GetMemberYearlyContributionByCountryAsync(newCountryID, newYear);
            ViewData["Years"] = new SelectList(GenerateYears.PopulateYears(), "", "", newYear);
            ViewBag.Error = error;
            var data = new MemberContributionView
            {
                Countries = countries.ToList(),
                Members = members.ToList(),
                SelectedCountryID = newCountryID,
                SelectedRegionID=regionID,
                SelectedStateID=contribution.StateID
            };
            return View("Index", data);
        }

        public async Task<IActionResult> EditCreateError(MemberDto data, bool isEdit)
        {
            try 
            {
                await ViewBagCache(data.StateID, data.PositionID);
                if (isEdit)
                {
                    return PartialView("_PartialEdit", data);
                }
                return PartialView("_PartialCreate", data);
            }
            catch (Exception exp)
            {
                return PartialView("Error", new ErrorViewModel { RequestId = exp.Message.ToString() });
            }
        }

        public IActionResult IndexMember(List<MemberYearlyContribution> members)
        {
            try
            {                
                return PartialView("_PartialListing", members);
            }
            catch (Exception exp)
            {
                return PartialView("Error", new ErrorViewModel { RequestId = exp.Message.ToString() });
            }
        }

        private async Task ViewBagCache(int stateID, int positionID)
        {
            var postisions = await _positionService.GetAllPositions();
            var states = await _stateService.GetAllStates();
            ViewData["PositionID"] = new SelectList(postisions.OrderBy(a => a.Name), "PositionID", "Name", positionID);
            ViewData["StateID"] = new SelectList(states.OrderBy(a => a.Name), "StateID", "Name", stateID);
        }
    }
}
