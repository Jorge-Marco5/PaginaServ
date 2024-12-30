// En AccountController.cs
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using MvcMovie.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
        private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View(); // Esto cargará la vista en Views/Account/Register.cshtml
    }

    public IActionResult crear_perfil()
    {
        return View(); // Esto cargará la vista en Views/Account/Register.cshtml
    }

[HttpPost]
    public IActionResult crear_perfil(Usuario usuario)
{
    //verificamos si el correo ingresado ya existe
    var correoExiste = _context.Usuarios.Any(u => u.Correo == usuario.Correo);

    if (correoExiste)
    {
        ModelState.AddModelError("Correo", "El correo ya está registrado, Ingrese otro.");
        return View(usuario); // Retornar la vista con los datos actuales y el mensaje de error
    }

    if (ModelState.IsValid){//Si los datos son validos los guardamos en la base de datos

        //Mostramos en la consola que los datos se obtienen correctamente
        //*Mantener esta parte oculta --> Console.WriteLine($"IdUsuario:{registrarUsuario.IdUsuario}, $Peso:{registrarUsuario.Peso}, $Edad:{registrarUsuario.Edad}, $Alergias:{registrarUsuario.Alergias}, $Enfermedades:{registrarUsuario.Enfermedades}, $FechaActualizacion:{registrarUsuario.FechaActualizacion}");

        //cargamos los datos del usuario en la base de datos
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        TempData["Mensaje"] = "¡Registro realizado correctamente! ingrese sus credenciales nuevamente";
        return RedirectToAction("Login", "Home"); //Redirigimos a la vista que muestra los datos del usuario
    }
    // Si no es válido, mostrar e1l mensaje de error
    //*Mantener oculta esta parte
    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
        ModelState.AddModelError("Correo", "¡Error al registrar los datos!. Intente nuevamente");
    }

    // Si hay errores de validación, devolver el formulario con los errores
    return View("crear_perfil");
}

    // GET: Muestra la vista para cambiar contraseña


}
