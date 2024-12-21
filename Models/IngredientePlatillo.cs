public class IngredientePlatillo
{
    public int IdIngredientePlatillo { get; set; } // Clave primaria y FK hacia Usuario
    public int IdPlatillo { get; set; }
    public int IdIngrediente { get; set; }
    public required string Cantidad { get; set; }

    public required Ingrediente Ingrediente { get; set; }
    public required Platillo Platillo { get; set; }
    
    public ICollection<IngredientePlatillo> IngredientesPlatillos { get; set; } = new List<IngredientePlatillo>();

}