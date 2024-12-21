using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

public class DatosUsuario : Controller
{
    public int Edad { get; private set; }
    public float Peso { get; private set; }
    public string? Enfermedad { get; private set; }
    public string? Ingredientes { get; private set; }

    [HttpPost]
    [Route("procesarFormulario")]
    public IActionResult ProcesarFormulario(DatosUsuario usuario)
    {
        // Accede a los datos enviados desde el formulario
        int edad = usuario.Edad;
        float peso = usuario.Peso;
        string? enfermedad = usuario.Enfermedad;
        string? ingredientes = usuario.Ingredientes;

        // Aquí puedes procesar los datos (guardar en la base de datos, etc.)

        // Retorna una respuesta, por ejemplo, una vista o un mensaje
        return Content($"Datos recibidos: Edad: {edad}, Peso: {peso}, Enfermedad: {enfermedad}, Ingredientes: {ingredientes}");
    }
}
