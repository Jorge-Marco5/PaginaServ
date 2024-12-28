using MvcMovie.Models;
public class DetalleRegistroConsumo
{
    public int IdDetalle { get; set; } // Clave primaria y FK hacia Usuario
    public int IdRegistro { get; set; }
    public int IdCategoria { get; set; }
    public required string Cantidad { get; set; }

        public Registro? Registro { get; set; } // Relación con Registro

    public CategoriaIngrediente? CategoriaIngrediente { get; set; } // Relación con CategoriaIngrediente


}