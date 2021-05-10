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
    public class ContributionsController : Controller
    {
        private readonly MemberShipContext _context;
        private readonly IMemberServices _memberService;
        private readonly IPositionServices _positionService;
        private readonly IStateServices _stateService;
        private readonly ICountryServices _countryService;
        private readonly IRegionServices _regionService;
        private readonly IContributionServices _contributionService;
        public ContributionsController(
            MemberShipContext context,
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
            _context = context;
        }
        public async Task<IActionResult> Indexation(int? year, int? countryID)
        {
            var countries = await _countryService.GetAllCountryWithConnect();
            int newCountryID = countryID.HasValue ? countryID.Value : countries.First().CountryID;
            int newYear = year.HasValue ? year.Value : DateTime.Now.Year;
            var members = await _memberService.GetMemberYearlyContributionByCountryAsync(newCountryID, newYear);
            ViewData["Years"] = new SelectList(PopulateYears(), "", "", newYear);
            var data = new MemberContributionView
            {
                Countries = countries.ToList(),
                Members = members.ToList(),
                SelectedCountryID = newCountryID
            };
            return View(data);
        }
        // GET: Contributions
        public async Task<IActionResult> Index()
        {
            var memberShipContext = _context.Contributions.Include(c => c.ContributionMethod).Include(c => c.Member);
            return View(await memberShipContext.ToListAsync());
        }

        // GET: Contributions/Details/5
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
                return View(member);
            }
            catch (Exception exp)
            {
                return PartialView("Error", new ErrorViewModel { RequestId = exp.Message.ToString() });
            }
        }


        // GET: Contributions/Create
        public IActionResult Create()
        {
            ViewData["ContributionMethodID"] = new SelectList(_context.ContributionMethods, "ContributionMethodID", "Name");
            ViewData["MemberID"] = new SelectList(_context.Members, "MemberID", "LastName");
            return View();
        }

        // POST: Contributions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContributionID,MemberID,ContributionMethodID,Amount,ContributionDate")] ContributionDto contribution)
        {
            string error;
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _contributionService.CreateContribution(contribution);
                    if (result.IsSuccessful())
                    {
                        return RedirectToAction(nameof(Indexation));
                    }
                    ModelState.AddModelError(string.Empty, result.ErrorMessages[0]);
                    error = result.ErrorMessages.Any() ? result.ErrorMessages[0] : "An error occured";
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message.ToString());
                    error= ex.Message.ToString();
                }
            }           

            error = ModelState.GetErrorsAsBRList().ToString();
            var countries = await _countryService.GetAllCountryWithConnect();
            int newCountryID = countries.First().CountryID;
            int newYear = DateTime.Now.Year;
            var members = await _memberService.GetMemberYearlyContributionByCountryAsync(newCountryID, newYear);
            ViewData["Years"] = new SelectList(PopulateYears(), "", "", newYear);
            ViewBag.Error = error;
            var data = new MemberContributionView
            {
                Countries = countries.ToList(),
                Members = members.ToList(),
                SelectedCountryID = newCountryID
            };
            return View("Indexation", data);
        }

        // GET: Contributions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _context.Contributions.FindAsync(id);
            if (contribution == null)
            {
                return NotFound();
            }
            ViewData["ContributionMethodID"] = new SelectList(_context.ContributionMethods, "ContributionMethodID", "Name", contribution.ContributionMethodID);
            ViewData["MemberID"] = new SelectList(_context.Members, "MemberID", "LastName", contribution.MemberID);
            return View(contribution);
        }

        // POST: Contributions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContributionID,MemberID,ContributionMethodID,Amount,ContributionDate")] Contribution contribution)
        {
            if (id != contribution.ContributionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contribution);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContributionExists(contribution.ContributionID))
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
            ViewData["ContributionMethodID"] = new SelectList(_context.ContributionMethods, "ContributionMethodID", "Name", contribution.ContributionMethodID);
            ViewData["MemberID"] = new SelectList(_context.Members, "MemberID", "LastName", contribution.MemberID);
            return View(contribution);
        }

        // GET: Contributions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contribution = await _context.Contributions
                .Include(c => c.ContributionMethod)
                .Include(c => c.Member)
                .FirstOrDefaultAsync(m => m.ContributionID == id);
            if (contribution == null)
            {
                return NotFound();
            }

            return View(contribution);
        }

        // POST: Contributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contribution = await _context.Contributions.FindAsync(id);
            _context.Contributions.Remove(contribution);
            await _context.SaveChangesAsync();
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

        private bool ContributionExists(int id)
        {
            return _context.Contributions.Any(e => e.ContributionID == id);
        }
        private List<int> PopulateYears()
        {
            List<int> lst = new List<int>();
            for(int min=DateTime.Today.Year -10; min<= DateTime.Today.Year; min++)
            {
                lst.Add(min);
            }
            return lst;

        }
    }
}
