using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NowyPrzewodnikMVC.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NowyPrzewodnikMVC.Controllers
{
    public class WalkController : Controller
    {
        private readonly AppDbContext _context;

        public WalkController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var startPlace = await _context.Waypoints.OrderBy(w => w.Id).FirstOrDefaultAsync();
                if (startPlace != null) return RedirectToAction("Index", new { id = startPlace.Id });
                return NotFound("Baza pusta.");
            }

            var waypoint = await _context.Waypoints
                .Include(w => w.OutboundConnections)
                    .ThenInclude(c => c.Target) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (waypoint == null) return NotFound();

            // Lista do teleportu
            ViewBag.AllDestinations = await _context.Waypoints
                .OrderBy(w => w.Id)
                .Select(w => new { w.Id, w.Name })
                .ToListAsync();

            return View(waypoint);
        }
    }
}