using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;
using MvcMovie.Models;
using DbContextNamespace = MvcMovie.Data;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContextNamespace.ApplicationDbContext? context;

        public ApplicationDbContext Context => context;

        public DbContextNamespace.ApplicationDbContext GetContext() => Context;

        public IActionResult Index()
        {
            return View();
        }

        /*public IActionResult Home()
        {
            return View();
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /* CONTROL DE LA CONEXIÓN A LA BASE DE DATOS */
        public IActionResult Home()
{
    try
    {
        // Contar registros en la tabla Usuarios
        var count = GetContext().Usuarios.Count(); // Verifica que Usuarios sea un DbSet en ApplicationDbContext
        return Content($"Conexión exitosa. Total registros en Usuarios: {count}");
    }
    catch (Exception ex)
    {
        return Content($"Error en la conexión: {ex.Message}");
    }
}

    }
}
