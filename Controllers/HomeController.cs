using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using app.web.Models;
using app.web.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace app.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DefaultDbContext _context;
        
        public HomeController(DefaultDbContext context)
        {
            _context = context;    
        }

        public IActionResult Index()
        {
            return View(_context.Roads
                .Include(x => x.Place1)
                .Include(x => x.Place2)
                .ToArray());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
