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
    public class MembersController : Controller
    {
        private readonly IMemberServices _memberService;
        private readonly IPositionServices _positionService;
        private readonly IStateServices _stateService;
        public MembersController(IMemberServices memberService, IPositionServices positionService, IStateServices stateService)
        {
            this._memberService = memberService;
            this._positionService = positionService;
            this._stateService = stateService;

        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var members = _memberService.GetAllConnectMembers();
            return View(await members);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberById(id.Value);
                //await _context.Members
                //.Include(m => m.Position)
                //.Include(m => m.State)
                //.FirstOrDefaultAsync(m => m.MemberID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public async Task<IActionResult> Create()
        {
            var postisions = await _positionService.GetAllPositions();
            var states = await _stateService.GetAllStates();
            ViewData["PositionID"] = new SelectList(postisions.OrderBy(a => a.Name), "PositionID", "Name");
            ViewData["StateID"] = new SelectList(states.OrderBy(a => a.Name), "StateID", "Name");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,PositionID,StateID,Code,FirtsName,LastName,Phone,Email,StartDate,EndDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                var result = await _memberService.CreateMember(member);
                if (result.IsSuccessful())
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, result.ErrorMessages[0]);
            }
            var postisions = await _positionService.GetAllPositions();
            var states = await _stateService.GetAllStates();
            ViewData["PositionID"] = new SelectList(postisions.OrderBy(a => a.Name), "PositionID", "Name", member.PositionID);
            ViewData["StateID"] = new SelectList(states.OrderBy(a => a.Name), "StateID", "Name", member.StateID);

           
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            var postisions = await _positionService.GetAllPositions();
            var states = await _stateService.GetAllStates();
            ViewData["PositionID"] = new SelectList(postisions.OrderBy(a => a.Name), "PositionID", "Name", member.PositionID);
            ViewData["StateID"] = new SelectList(states.OrderBy(a => a.Name), "StateID", "Name", member.StateID);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberID,PositionID,StateID,Code,FirtsName,LastName,Phone,Email,StartDate,EndDate")] Member member)
        {
            if (id != member.MemberID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _memberService.UpdateMember(id, member);
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

            var postisions = await _positionService.GetAllPositions();
            var states = await _stateService.GetAllStates();
            ViewData["PositionID"] = new SelectList(postisions.OrderBy(a => a.Name), "PositionID", "Name", member.PositionID);
            ViewData["StateID"] = new SelectList(states.OrderBy(a => a.Name), "StateID", "Name", member.StateID);
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _memberService.GetMemberById(id.Value);
                
                //.Members
                //.Include(m => m.Position)
                //.Include(m => m.State)
                //.FirstOrDefaultAsync(m => m.MemberID == id);
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

        //private bool MemberExists(int id)
        //{
        //    return _context.Members.Any(e => e.MemberID == id);
        //}
    }
}
