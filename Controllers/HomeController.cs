using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;
using MvcMovie.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    //Controlador que muestra la vista de la pagina Login
    public IActionResult Login()
    {
        return View();
    }

       // Acción POST de Login
        [HttpPost]
        //Controlador del login, realiza la verificacion del inicio de sesión
        //obteniendo el nombre del usuario y correo electronico registrados.
        public IActionResult Login(string Correo, string Contrasena)
        {
            // Buscar usuario en la base de datos
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Correo == Correo);

            if (usuario != null && usuario.Contrasena == Contrasena)
            {
                // Guardar información en la sesión (opcionalmente, puedes usar cookies o un token)
                HttpContext.Session.SetInt32("UsuarioId" ,usuario.IdUsuario);
                HttpContext.Session.SetString("Correo",usuario.Correo);
                HttpContext.Session.SetString("UsuarioNombre", usuario.UsuarioNombre);

                // Redirigir al Home
                return RedirectToAction("Home", "Home");

            }
            else
            {
                // Si no se encontró el usuario o la contraseña es incorrecta, mostrar mensaje de error
                TempData["ErrorMessage"] = "Credenciales incorrectas.";
                return RedirectToAction("Login");
            }
        }

     // Página Home
        public IActionResult Home()
        {
            var Correo = HttpContext.Session.GetString("Correo");
            var UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(UsuarioNombre)){
            // Si no hay datos, redirigir al login
                return RedirectToAction("Login", "Login");
            }

            // Pasar los datos a la vista
            ViewBag.Correo = Correo;
            ViewBag.UsuarioNombre = UsuarioNombre;

            return View();

        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    //Controlador para verificar la conexion a la base de datos,
    //y realiza un conteo de los usuarios registrados
    public IActionResult Conexion()
    {
        try
        {
            // Contar registros en la tabla Usuarios
            var count = _context.Usuarios.Count();
            return Content($"Conexión exitosa. Total registros en Usuarios: {count}");
        }
        catch (Exception ex)
        {
            return Content($"Error en la conexión: {ex.Message}");
        }
    }
}
