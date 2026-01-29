using Microsoft.AspNetCore.Authentication.Cookies; // <--- WAŻNE
using Microsoft.EntityFrameworkCore;
using NowyPrzewodnikMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Serwisy
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- KONFIGURACJA LOGOWANIA (CIASTECZKA) ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login"; // Gdzie przekierować niezalogowanego?
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Sesja trwa 20 min
    });
// -------------------------------------------

var app = builder.Build();

// 2. Seedowanie danych (bez zmian)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try { SeedData.Initialize(services); }
    catch (Exception ex) { 
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Błąd seedowania."); 
    }
}

// 3. Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

// --- WAŻNA KOLEJNOŚĆ ---
app.UseAuthentication(); // 1. Sprawdź kim jestem
app.UseAuthorization();  // 2. Sprawdź czy mam dostęp
// -----------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Walk}/{action=Index}/{id?}");

app.Run();