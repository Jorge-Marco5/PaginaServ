using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

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
                return RedirectToAction("Login", "Home");
            }
        }

     // Página Home
    public IActionResult Home()
    {
        var Correo = HttpContext.Session.GetString("Correo");
        var UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

        if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(UsuarioNombre)){
        // Si no hay datos, redirigir al login
            return RedirectToAction("Login", "Home");
        }

        // Pasar los datos a la vista
        ViewBag.Correo = Correo;
        ViewBag.UsuarioNombre = UsuarioNombre;

        return View();

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

    //MOSTRAMOS LA VISTA PARA CAMBIAR LA CONTRASEÑA.
    public IActionResult cambiar_contraseña()
    {
        return View();
    }
    //LOGICA DE LA APLICACION PARA CAMBIAR LA CONTRASEÑA DE CADA USUARIO
    [HttpPost]
    public async Task<IActionResult> cambiar_contraseña(string correo, string contrasena_Actual, string nueva_contrasena , string confirmar_contraseña)
    {
        if (
            string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(contrasena_Actual) ||
            string.IsNullOrWhiteSpace(nueva_contrasena) ||
            string.IsNullOrWhiteSpace(confirmar_contraseña))
        {
            ViewBag.MensajeCambiarContraseña = "Todos los campos son obligatorios.";
            return View();
        }

        Console.WriteLine($"Correo:{correo}, $Contraseña Actual:{contrasena_Actual}, $Nueva contraseña:{nueva_contrasena}, $Confirmar contraseña:{confirmar_contraseña}");

        // Busca el usuario en la base de datos
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);

        if (usuario == null)
        {
            ViewBag.MensajeCambiarContraseña = "Usuario no encontrado.";
            return View();
        }

        // Verifica que la contraseña actual coincida
        if (usuario.Contrasena != contrasena_Actual)
        {
            ViewBag.MensajeCambiarContraseña = "La contraseña actual es incorrecta.";
            return View();
        }

        //Verificar que las nuevas contraseñas coincidan
        if (nueva_contrasena != confirmar_contraseña)
        {
            ViewBag.MensajeCambiarContraseña = "Las contraseñas nuevas no coinciden";
            return View();
        }

        // Cambia la contraseña
        usuario.Contrasena = confirmar_contraseña;

        // Guarda los cambios en la base de datos
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();

        ViewBag.MensajeCambiarContraseña = "Contraseña actualizada con éxito.";
        return View();
        //RedirectToAction("Login", "Home");
    }

}
