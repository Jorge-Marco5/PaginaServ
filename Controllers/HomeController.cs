using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System.Linq; // Asegúrate de que estás usando el namespace correcto para LINQ
using DbContextNamespace = ApplicationDbContext.Data; // Esto se debe mover fuera de la clase

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbContextNamespace.ApplicationDbContext _context; // Cambiado a usar el alias DbContextNamespace

        public HomeController(ILogger<HomeController> logger, DbContextNamespace.ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult home()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*CONTROL DE LA CONEXION A LA BASE DE DATOS*/
        public IActionResult TestConexion()
        {
            try
            {
                // Prueba básica: intenta contar registros de una tabla
                var count = _context.Usuarios.Count(); // Asumiendo que Usuarios es una tabla en tu base de datos
                return Content($"Conexión exitosa. Total registros en Usuarios: {count}");
            }
            catch (Exception ex)
            {
                return Content($"Error en la conexión: {ex.Message}");
            }
        }
    }
}
