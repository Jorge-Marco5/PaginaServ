// En AccountController.cs
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View(); // Esto cargará la vista en Views/Account/Register.cshtml
    }

    public
     IActionResult crear_perfil()
    {
        return View(); // Esto cargará la vista en Views/Account/Register.cshtml
    }

    public
     IActionResult cambiar_contraseña()
    {
        return View(); // Esto cargará la vista en Views/Account/Register.cshtml
    }
}
