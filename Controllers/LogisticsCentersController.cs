using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.web.Data;
using app.web.Models.Entities;
using app.web.Infrastructure.Contracts;

namespace app.web.Controllers
{
    public class LogisticsCentersController : Controller
    {
        private readonly ILogisticsCenterService _service;

        private readonly DefaultDbContext _context;

        public LogisticsCentersController(DefaultDbContext context, ILogisticsCenterService service)
        {
            _context = context;
            _service = service;
        }

        // GET: LogisticsCenters
        public async Task<IActionResult> Index()
        {
            var defaultDbContext = _context.LogisticsCenters.Include(l => l.Place);
            return View(await defaultDbContext.ToListAsync());
        }

        // GET: LogisticsCenters/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logisticsCenter = await _context.LogisticsCenters
                .Include(l => l.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logisticsCenter == null)
            {
                return NotFound();
            }

            return View(logisticsCenter);
        }

        // GET: LogisticsCenters/Create
        public IActionResult Create()
        {
            ViewData["PlaceId"] = new SelectList(_context.Places, "Id", "Id");
            return View();
        }

        // POST: LogisticsCenters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlaceId")] LogisticsCenter logisticsCenter)
        {
            if (!ModelState.IsValid) return View(logisticsCenter);

            try
            {
                await _service.TryCreateLogisticsCenter();
            }
            catch (System.Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(logisticsCenter);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LogisticsCenters/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logisticsCenter = await _context.LogisticsCenters
                .Include(l => l.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logisticsCenter == null)
            {
                return NotFound();
            }

            return View(logisticsCenter);
        }

        // POST: LogisticsCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var logisticsCenter = await _context.LogisticsCenters.FindAsync(id);
            _context.LogisticsCenters.Remove(logisticsCenter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogisticsCenterExists(uint id)
        {
            return _context.LogisticsCenters.Any(e => e.Id == id);
        }
    }
}
