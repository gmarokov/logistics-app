using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.web.Data;
using app.web.Models.Entities;

namespace app.web.Controllers
{
    public class RoadsController : Controller
    {
        private readonly DefaultDbContext _context;

        public RoadsController(DefaultDbContext context)
        {
            _context = context;
        }

        // GET: Roads
        public async Task<IActionResult> Index()
        {
            var defaultDbContext = _context.Roads.Include(r => r.Place1).Include(r => r.Place2);
            return View(await defaultDbContext.ToListAsync());
        }

        // GET: Roads/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var road = await _context.Roads
                .Include(r => r.Place1)
                .Include(r => r.Place2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (road == null)
            {
                return NotFound();
            }

            return View(road);
        }

        // GET: Roads/Create
        public IActionResult Create()
        {
            ViewData["Place1Id"] = new SelectList(_context.Places, "Id", "Name");
            ViewData["Place2Id"] = new SelectList(_context.Places, "Id", "Name");
            return View();
        }

        // POST: Roads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Distance,Place1Id,Place2Id")] Road road)
        {
            ViewData["Place1Id"] = new SelectList(_context.Places, "Id", "Name", road.Place1Id);
            ViewData["Place2Id"] = new SelectList(_context.Places, "Id", "Name", road.Place2Id);

            if (ModelState.IsValid)
            {
                if (road.Place1Id == road.Place2Id)
                {
                    ModelState.AddModelError(string.Empty, "Can't create road between same place.");
                    return View(road);
                }

                _context.Add(road);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(road);
        }

        // GET: Roads/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var road = await _context.Roads.FindAsync(id);
            ViewData["Place1Id"] = new SelectList(_context.Places, "Id", "Name", road.Place1Id);
            ViewData["Place2Id"] = new SelectList(_context.Places, "Id", "Name", road.Place2Id);

            if (road == null)
            {
                return NotFound();
            }

            return View(road);
        }

        // POST: Roads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,Distance,Place1Id,Place2Id")] Road road)
        {
            if (id != road.Id)
            {
                return NotFound();
            }

            ViewData["Place1Id"] = new SelectList(_context.Places, "Id", "Name", road.Place1Id);
            ViewData["Place2Id"] = new SelectList(_context.Places, "Id", "Name", road.Place2Id);

            if (ModelState.IsValid)
            {
                if (road.Place1Id == road.Place2Id)
                {
                    ModelState.AddModelError(string.Empty, "Can't create road between same place.");
                    return View(road);
                }

                try
                {
                    _context.Update(road);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoadExists(road.Id))
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
            
            return View(road);
        }

        // GET: Roads/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var road = await _context.Roads
                .Include(r => r.Place1)
                .Include(r => r.Place2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (road == null)
            {
                return NotFound();
            }

            return View(road);
        }

        // POST: Roads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var road = await _context.Roads.FindAsync(id);
            _context.Roads.Remove(road);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoadExists(uint id) => _context.Roads.Any(e => e.Id == id);
    }
}
