// Modelo para CategoriaIngrediente
public class CategoriaIngrediente
{
    public int IdCategoria { get; set; } // Clave primaria
    public required string Nombre { get; set; }

    // Relación uno a muchos con Ingredientes
    public required ICollection<Ingrediente> Ingredientes { get; set; }

    public ICollection<DetalleRegistroConsumo> DetalleRegistroConsumos { get; set; } = new List<DetalleRegistroConsumo>();
    
}