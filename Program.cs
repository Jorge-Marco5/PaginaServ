using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuración del contexto y cadena de conexión
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=BdPrueba;User Id=Admin1;Password=142753869;TrustServerCertificate=True;",
    sqlOptions => sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5, // Número máximo de reintentos
        maxRetryDelay: TimeSpan.FromSeconds(10), // Retraso máximo entre reintentos
        errorNumbersToAdd: null // Puedes agregar códigos de error específicos
    )));

// Agregar soporte para sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de inactividad
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Redireccionamiento de solicitudes HTTP
app.UseHttpsRedirection();
app.UseStaticFiles();


// Habilitar el uso de sesiones
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
