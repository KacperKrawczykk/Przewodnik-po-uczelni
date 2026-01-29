using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NowyPrzewodnikMVC.Data;
using NowyPrzewodnikMVC.Models;

namespace NowyPrzewodnikMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // --- DASHBOARD ---
        public async Task<IActionResult> Index()
        {
            var waypoints = await _context.Waypoints.OrderBy(w => w.Id).ToListAsync();
            return View(waypoints);
        }

        // --- CREATE (GET) ---
        public IActionResult Create()
        {
            return View();
        }

        // --- CREATE (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Dodano Description do Bind
        public async Task<IActionResult> Create([Bind("Name,Description")] Waypoint waypoint, IFormFile? imageFile)
        {
            // Obsługa pliku
            if (imageFile != null && imageFile.Length > 0)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                string extension = Path.GetExtension(imageFile.FileName);
                fileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/media/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                waypoint.ImageUrl = "/media/" + fileName;
            }
            else
            {
                waypoint.ImageUrl = "https://placehold.co/600x400?text=Brak+Zdjecia";
            }

            if (ModelState.IsValid)
            {
                _context.Add(waypoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(waypoint);
        }

        // --- EDIT (GET) ---
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var waypoint = await _context.Waypoints.FindAsync(id);
            if (waypoint == null) return NotFound();
            return View(waypoint);
        }

        // --- EDIT (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Dodano Description do Bind
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl")] Waypoint waypoint, IFormFile? imageFile)
        {
            if (id != waypoint.Id) return NotFound();

            if (ModelState.IsValid)
            {
                // Jeśli wgrano nowe zdjęcie
                if (imageFile != null && imageFile.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                    string extension = Path.GetExtension(imageFile.FileName);
                    fileName = fileName + "_" + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/media/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    waypoint.ImageUrl = "/media/" + fileName;
                }

                _context.Update(waypoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(waypoint);
        }

        // --- DELETE ---
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var waypoint = await _context.Waypoints.FindAsync(id);
            if (waypoint != null)
            {
                var connections = _context.Connections.Where(c => c.SourceId == id || c.TargetId == id);
                _context.Connections.RemoveRange(connections);
                _context.Waypoints.Remove(waypoint);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // --- CONNECTIONS ---
        public async Task<IActionResult> Connections(int id)
        {
            var sourcePlace = await _context.Waypoints
                .Include(w => w.OutboundConnections).ThenInclude(c => c.Target)
                .FirstOrDefaultAsync(w => w.Id == id);
            if (sourcePlace == null) return NotFound();
            ViewBag.AllWaypoints = await _context.Waypoints.OrderBy(w => w.Name).ToListAsync();
            return View(sourcePlace);
        }

        [HttpPost]
        public async Task<IActionResult> AddConnection(int sourceId, int targetId, string direction)
        {
            if (sourceId == targetId) return RedirectToAction("Connections", new { id = sourceId });
            _context.Connections.Add(new Connection { SourceId = sourceId, TargetId = targetId, Direction = direction });
            await _context.SaveChangesAsync();
            return RedirectToAction("Connections", new { id = sourceId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConnection(int connectionId)
        {
            var conn = await _context.Connections.FindAsync(connectionId);
            if (conn != null) {
                int sourceId = conn.SourceId;
                _context.Connections.Remove(conn);
                await _context.SaveChangesAsync();
                return RedirectToAction("Connections", new { id = sourceId });
            }
            return RedirectToAction(nameof(Index));
        }

        // --- LOGOWANIE ---
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, "Admin") };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Błędny login lub hasło";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}