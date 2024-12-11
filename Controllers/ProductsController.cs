// En AccountController.cs
using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    public IActionResult vistaMenus()
    {
        return View(); // Cargará Views/products/vistaMenus.cshtml
    }

    public IActionResult registrar()
    {
        return View(); // Cargará Views/Products/registrar.cshtml
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