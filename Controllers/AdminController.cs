using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NowyPrzewodnikMVC.Data;
using NowyPrzewodnikMVC.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NowyPrzewodnikMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // 1. LISTA MIEJSC
        public async Task<IActionResult> Index()
        {
            return View(await _context.Waypoints.ToListAsync());
        }

        // 2. EDYCJA POŁĄCZEŃ
        public async Task<IActionResult> Connections(int id)
        {
            var waypoint = await _context.Waypoints
                .Include(w => w.OutboundConnections)
                    .ThenInclude(c => c.Target) 
                .FirstOrDefaultAsync(w => w.Id == id);

            if (waypoint == null) return NotFound();

            ViewBag.AllWaypoints = await _context.Waypoints.OrderBy(w => w.Name).ToListAsync();
            return View(waypoint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddConnection(int sourceId, int targetId, string direction)
        {
            var exists = await _context.Connections.AnyAsync(c => c.SourceId == sourceId && c.TargetId == targetId);
            if (!exists) {
                _context.Connections.Add(new Connection { SourceId = sourceId, TargetId = targetId, Direction = direction });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Connections", new { id = sourceId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConnection(int connectionId)
        {
            var conn = await _context.Connections.FindAsync(connectionId);
            if (conn != null) {
                int src = conn.SourceId;
                _context.Connections.Remove(conn);
                await _context.SaveChangesAsync();
                return RedirectToAction("Connections", new { id = src });
            }
            return RedirectToAction("Index");
        }

        // 3. CREATE (Z UPLOADEM DO /media)
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Waypoint waypoint, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    
                    // Unikalna nazwa
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    
                    // ZAPIS DO FOLDERU MEDIA
                    string path = Path.Combine(wwwRootPath, "media", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // ZAPIS ŚCIEŻKI W BAZIE
                    waypoint.ImageUrl = "/media/" + fileName;
                }
                else
                {
                    // Domyślny obrazek, jeśli brak pliku
                    waypoint.ImageUrl = "https://placehold.co/600x400?text=Brak+Zdjecia";
                }

                _context.Add(waypoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(waypoint);
        }

        // 4. EDIT (Z UPLOADEM DO /media)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var waypoint = await _context.Waypoints.FindAsync(id);
            if (waypoint == null) return NotFound();
            return View(waypoint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Waypoint waypoint, IFormFile? imageFile)
        {
            if (id != waypoint.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                        string extension = Path.GetExtension(imageFile.FileName);
                        
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        
                        // ZAPIS DO FOLDERU MEDIA
                        string path = Path.Combine(wwwRootPath, "media", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        // AKTUALIZACJA ŚCIEŻKI W BAZIE
                        waypoint.ImageUrl = "/media/" + fileName;
                    }
                    else
                    {
                        // Jeśli nie wybrano pliku, zachowaj stare zdjęcie
                        _context.Entry(waypoint).Property(x => x.ImageUrl).IsModified = false;
                    }

                    _context.Update(waypoint);
                    
                    if (imageFile == null) 
                    {
                        _context.Entry(waypoint).Property(x => x.ImageUrl).IsModified = false;
                    }
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Waypoints.Any(e => e.Id == waypoint.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(waypoint);
        }

        // 5. DELETE (BEZPIECZNE USUWANIE)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var waypoint = await _context.Waypoints.FindAsync(id);
            if (waypoint != null)
            {
                // Najpierw usuń połączenia
                var outC = _context.Connections.Where(c => c.SourceId == id); 
                _context.Connections.RemoveRange(outC);
                
                var inC = _context.Connections.Where(c => c.TargetId == id); 
                _context.Connections.RemoveRange(inC);
                
                // Potem usuń miejsce
                _context.Waypoints.Remove(waypoint);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login() => View();
        public IActionResult Logout() => RedirectToAction("Index", "Walk");
    }
}