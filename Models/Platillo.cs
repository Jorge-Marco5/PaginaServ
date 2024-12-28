using MvcMovie.Models;

namespace MvcMovie.Models{

public class Platillo
{
    public int IdPlatillo { get; set; }
    public string? Nombre { get; set; }
    public string? Porcion { get; set; }
    public  string? Descripcion { get; set; }
    public string? Imagen { get; set; }
    public  string? TipoPlatillo { get; set; }

    public required ICollection<IngredientePlatillo> IngredientesPlatillos { get; set; }

}

}