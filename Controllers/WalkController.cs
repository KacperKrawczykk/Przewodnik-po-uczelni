using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NowyPrzewodnikMVC.Data;
using NowyPrzewodnikMVC.Models;

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
            // 1. Przekierowanie na start, jeśli brak ID
            if (id == null)
            {
                var startPlace = await _context.Waypoints.OrderBy(w => w.Id).FirstOrDefaultAsync();
                if (startPlace != null) return RedirectToAction("Index", new { id = startPlace.Id });
                return NotFound("Baza pusta.");
            }

            // 2. Pobieramy AKTUALNE miejsce
            var waypoint = await _context.Waypoints
                .Include(w => w.OutboundConnections)
                    .ThenInclude(c => c.Target) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (waypoint == null) return NotFound();

            // 3. LISTA ASYSTENTA (TELEPORT)
            // ZMIANA: Sortujemy po ID (w.Id), a nie po nazwie (w.Name)
            // Dzięki temu "Wejście" (ID 1) będzie zawsze pierwsze na liście.
            ViewBag.AllDestinations = await _context.Waypoints
                .OrderBy(w => w.Id) 
                .Select(w => new { w.Id, w.Name }) 
                .ToListAsync();

            return View(waypoint);
        }
    }
}