// En AccountController.cs
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Data;
using MvcMovie.Models;
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult vistaMenus()//Carga la vista con los menus
    {
        return View();
    }

    public IActionResult registrar()//Pagina de registro con los datos personales del usuario
    {
        var id = HttpContext.Session.GetInt32("UsuarioId");

        var usuario = _context.Salud.FirstOrDefault(u => u.IdUsuario == id); // Consulta los datos del usuario actual

        if (usuario == null)
        {
            //Si no hay datos de ese usuario en la base de datos, redirige al formulario de registro
            return RedirectToAction("RegistrarVacio");
        }else{
            //Si hay datos del usuario actual dentro de la base de datos, muestra esos datos.
            //Consulta los datos del usuario dentro de la base de datos.
            var datosUsuario = _context.Salud
                .Where(s => s.IdUsuario == id)
                .ToList();

            return View(datosUsuario);// Carga la pagina con los datos
        }

    }

    public IActionResult RegistrarVacio(){

            return View();// Cargamos la vista con el formulario de registro.
    }

[HttpPost]
public IActionResult RegistrarVacio(DatosSaludUsuario registrarUsuario)
{
    //carga el ID del usuario actual
    var id = HttpContext.Session.GetInt32("UsuarioId");
    //En caso de que no haya un ID en la sesion, redirige a pagina del Login
    if (id != null)
    {
            return RedirectToAction("login");
    }
    //Obtenemos los datos del usuario que ingreso al formulario
    if (ModelState.IsValid)
    {
        //Obtenemos la fecha actual
        registrarUsuario.FechaActualizacion = DateTime.Now.Date;

        //Mostramos en la consola que los datos se obtienen correctamente
        //*Mantener esta parte oculta --> Console.WriteLine($"IdUsuario:{registrarUsuario.IdUsuario}, $Peso:{registrarUsuario.Peso}, $Edad:{registrarUsuario.Edad}, $Alergias:{registrarUsuario.Alergias}, $Enfermedades:{registrarUsuario.Enfermedades}, $FechaActualizacion:{registrarUsuario.FechaActualizacion}");

        //cargamos los datos del usuario en la base de datos
        _context.Salud.Add(registrarUsuario); // Agregar al contexto
        _context.SaveChanges(); // Guardar los cambios
        return RedirectToAction("Registrar"); //Redirigimos a la vista que muestra los datos del usuario
    }

    // Si no es válido, mostrar los errores de validación en la consola
    //*Mantener oculta esta parte
    /*foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
        Console.WriteLine(error.ErrorMessage);
    }*/

    // Si hay errores de validación, devolver el formulario con los errores
    return View("RegistrarVacio");
}

     public IActionResult registro()
    {
        return View(); // Cargará Views/Products/registro.cshtml
    }

    public IActionResult consultar()
    {
        return View(); // Cargará Views/Products/registro.cshtml
    }
}