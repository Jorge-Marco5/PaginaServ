// Modelo para Ingrediente
public class Ingrediente
{
    public int IdIngrediente { get; set; } // Clave primaria
    public int IdCategoria { get; set; } // FK hacia CategoriaIngrediente
    public required string Nombre { get; set; }

    // Relación muchos a uno con CategoriaIngrediente
    public required CategoriaIngrediente CategoriaIngrediente { get; set; }
}