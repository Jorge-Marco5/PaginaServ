public class Platillo
{
    public int IdPlatillo { get; set; }
    public required string Nombre { get; set; }
    public required string Porcion { get; set; }
    public required string Descripcion { get; set; }
    public required string Imagen { get; set; }
    public ICollection<IngredientePlatillo> IngredientesPlatillos { get; set; } = new List<IngredientePlatillo>();
}