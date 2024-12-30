// En AccountController.cs
using MvcMovie.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        //Validacion de login
        var id = HttpContext.Session.GetInt32("UsuarioId");
        var Correo = HttpContext.Session.GetString("Correo");
        var UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");
        if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(UsuarioNombre)){
        // Si no hay datos, redirigir al login
            return RedirectToAction("Login", "Home");
        }

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

        var Correo = HttpContext.Session.GetString("Correo");
        var UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

        if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(UsuarioNombre)){
        // Si no hay datos, redirigir al login
            return RedirectToAction("Login", "Home");
        }
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

    public IActionResult registro()//Ingresa los datos de los Ingredientes con los que cuenta el usuario
    {
        return View(); // Cargará Views/Products/registro.cshtml
    }

[HttpPost]
public IActionResult registro(string Verduras, string Frutas, string Proteinas, string Condimentos)
{
    var Correo = HttpContext.Session.GetString("Correo");
    var UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

    if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(UsuarioNombre)){
    // Si no hay datos, redirigir al login
        return RedirectToAction("Login", "Home");
    }

    var ingredientes = new List<string>();

    // Procesar cada categoría ingresada por el usuario
    if (!string.IsNullOrWhiteSpace(Verduras))
        ingredientes.AddRange(Verduras.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()));

    if (!string.IsNullOrWhiteSpace(Frutas))
        ingredientes.AddRange(Frutas.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()));

    if (!string.IsNullOrWhiteSpace(Proteinas))
        ingredientes.AddRange(Proteinas.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()));

    if (!string.IsNullOrWhiteSpace(Condimentos))
        ingredientes.AddRange(Condimentos.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()));

    // Guardar los ingredientes en la sesión para usar en la consulta
    HttpContext.Session.SetString("IngredientesUsuario", string.Join(",", ingredientes));

    TempData["Mensaje"] = "Ingredientes registrados correctamente.";
    return RedirectToAction("consultar", "Products"); // Redirigir al controlador de consulta
}

    /*public IActionResult consultar()
    {
        return View(); // CargaráS Views/Products/registro.cshtml
    }*/

public IActionResult Consultar()
{
    var Correo = HttpContext.Session.GetString("Correo");
    var UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

    if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(UsuarioNombre)){
    // Si no hay datos, redirigir al login
        return RedirectToAction("Login", "Home");
    }

    // Recuperar los ingredientes ingresados por el usuario desde la sesión
    var ingredientesString = HttpContext.Session.GetString("IngredientesUsuario");

    if (string.IsNullOrEmpty(ingredientesString))
    {
        TempData["MensajeIngredientes"] = "No se ingresaron ingredientes.";
        return RedirectToAction("registro", "Products");
    }

    // Lista de ingredientes ingresados por el usuario
    var ingredientes = ingredientesString.Split(',').Select(i => i.Trim()).ToList();

    // Capitalizar cada ingrediente (primera letra en mayúscula y el resto en minúscula)
    var ingredientesCapitalizados = ingredientes.Select(i => CapitalizarPalabras(i)).ToList();

    // Obtener todos los platillos junto con sus ingredientes
    var platillosConIngredientes = _context.Platillos
        .Include(p => p.IngredientesPlatillos)
            .ThenInclude(ip => ip.Ingrediente)
        .ToList();

    // Filtrar los platillos en memoria para obtener aquellos que contengan TODOS los ingredientes requeridos
    var platillosFiltrados = platillosConIngredientes
        .Where(p => p.IngredientesPlatillos
            .All(ip => ingredientesCapitalizados.Contains(ip.Ingrediente.Nombre)) &&
            p.IngredientesPlatillos.Any(ip => ingredientesCapitalizados.Contains(ip.Ingrediente.Nombre))) // Verificar que tengan ingredientes
        .GroupBy(p => p.TipoPlatillo)
        .Select(g => new CategoriaPlatillosViewModel
        {
            Categoria = g.Key,
            Platillos = g.Select(p => new PlatilloViewModel
            {
                Nombre = p.Nombre,
                Porcion = p.Porcion ?? "Sin especificar",
                Ingredientes = string.Join(", ", p.IngredientesPlatillos.Select(ip => ip.Ingrediente.Nombre)),
                Descripcion = p.Descripcion ?? "Descripción no disponible",
                Imagen = p.Imagen ?? "default-image.jpg"
            }).ToList()
        })
        .Where(categoria => categoria.Platillos.Any()) // Verificar que haya platillos en la categoría
        .ToList();

    if (!platillosFiltrados.Any())
    {
        TempData["MensajeIngredientes"] = "No se encontraron platillos con los ingredientes proporcionados";
                return RedirectToAction("registro", "Products");

    }
    else
    {
        ViewBag.Mensaje = null;
    }

    // Mostrar resultados en consola para depuración
    foreach (var categoria in platillosFiltrados)
    {
        Console.WriteLine($"Categoría: {categoria.Categoria}");
        foreach (var platillo in categoria.Platillos)
        {
            Console.WriteLine($"Platillo: {platillo.Nombre}, Ingredientes: {platillo.Ingredientes}");
        }
    }

    return View(platillosFiltrados);
}


// Función que capitaliza la primera letra de cada palabra
private string CapitalizarPalabras(string texto)
{
    var palabras = texto.Split(' ');

    for (int i = 0; i < palabras.Length; i++)
    {
        if (palabras[i].Length > 0)
        {
            palabras[i] = char.ToUpper(palabras[i][0]) + palabras[i].Substring(1).ToLower();
        }
    }

    return string.Join(" ", palabras);
}

}