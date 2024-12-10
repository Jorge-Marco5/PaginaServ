// En AccountController.cs
using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    public IActionResult vistaMenus()
    {
        return View(); // Cargará Views/Account/Register.cshtml
    }
}