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

    public IActionResult vistaMenus()
    {
        return View(); // Cargará Views/products/vistaMenus.cshtml
    }

    public IActionResult registrar()//Pagina de registro con los datos personales del usuario
    {
    var id = HttpContext.Session.GetInt32("UsuarioId");

    var usuario = _context.Salud.FirstOrDefault(u => u.IdUsuario == id); // Busca un usuario por ID

    if (usuario == null)
    {
         return RedirectToAction("RegistrarVacio");
    }
    return View(usuario);// Cargará Views/Products/registrar.cshtml

    }

    public IActionResult RegistrarVacio(){

        return View();// Cargará Views/Products/registrar.cshtml
    }

[HttpPost]
public IActionResult RegistrarVacio(DatosSaludUsuario registrarUsuario)
{
    var id = HttpContext.Session.GetInt32("UsuarioId");
    var idSalud = HttpContext.Session.GetInt32("UsuarioId");

    

    if (id != null)
    {
        registrarUsuario.IdUsuario = id.Value; // Asignar el ID del usuario a la entidad
        //registrarUsuario.IdSalud = id.Value; // Asignar el ID del usuario a la entidad
    }
    
    if (ModelState.IsValid)
    {
        Console.WriteLine($"IdSalud:{registrarUsuario.IdSalud}, $IdUsuario:{registrarUsuario.IdUsuario}, $Peso:{registrarUsuario.Peso}, $Edad:{registrarUsuario.Edad}, $Alergias:{registrarUsuario.Alergias}, $Enfermedades:{registrarUsuario.Enfermedades}");

        _context.Salud.Add(registrarUsuario); // Agregar al contexto
        _context.SaveChanges(); // Guardar los cambios
        return RedirectToAction("Registrar"); // O a la vista que quieras redirigir
    }

    // Si no es válido, mostrar los errores de validación en la consola
    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
        Console.WriteLine(error.ErrorMessage);
    }

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