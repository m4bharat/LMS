using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackOffice.Data;
using LotteryAPI.DbInfra.Model;

namespace BackOffice.Controllers
{
    public class ContestDetailsController : Controller
    {
        private readonly BackOfficeContext _context;

        public ContestDetailsController(BackOfficeContext context)
        {
            _context = context;
        }

        // GET: ContestDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContestDetails.ToListAsync());
        }

        // GET: ContestDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contestDetail = await _context.ContestDetails
                .FirstOrDefaultAsync(m => m.ContestDetailId == id);
            if (contestDetail == null)
            {
                return NotFound();
            }

            return View(contestDetail);
        }

        // GET: ContestDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContestDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContestDetailId,ContestDetailBannerImgUrl,ContestDetailTileImgUrl,ContestDetailTitle,ContestDetailDescription,ContestTicketPrice,ContestTotalTicket,ContestTotalBoughtTicket,ContestValue,ContestStartDate,ContestStartTime,ContestEndDate,ContestEndTime,ContestDrawDate,ContestDrawTime,ContestNumberOfWinners,IsResultPublished,DrawContestNumbers,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy,IsDeleted,IsActive")] ContestDetail contestDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contestDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contestDetail);
        }

        // GET: ContestDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contestDetail = await _context.ContestDetails.FindAsync(id);
            if (contestDetail == null)
            {
                return NotFound();
            }
            return View(contestDetail);
        }

        // POST: ContestDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContestDetailId,ContestDetailBannerImgUrl,ContestDetailTileImgUrl,ContestDetailTitle,ContestDetailDescription,ContestTicketPrice,ContestTotalTicket,ContestTotalBoughtTicket,ContestValue,ContestStartDate,ContestStartTime,ContestEndDate,ContestEndTime,ContestDrawDate,ContestDrawTime,ContestNumberOfWinners,IsResultPublished,DrawContestNumbers,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy,IsDeleted,IsActive")] ContestDetail contestDetail)
        {
            if (id != contestDetail.ContestDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contestDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContestDetailExists(contestDetail.ContestDetailId))
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
            return View(contestDetail);
        }

        // GET: ContestDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contestDetail = await _context.ContestDetails
                .FirstOrDefaultAsync(m => m.ContestDetailId == id);
            if (contestDetail == null)
            {
                return NotFound();
            }

            return View(contestDetail);
        }

        // POST: ContestDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contestDetail = await _context.ContestDetails.FindAsync(id);
            if (contestDetail != null)
            {
                _context.ContestDetails.Remove(contestDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContestDetailExists(int id)
        {
            return _context.ContestDetails.Any(e => e.ContestDetailId == id);
        }
    }
}
