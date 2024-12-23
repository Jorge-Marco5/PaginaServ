using MvcMovie.Models;

public class Registro
{
    public int IdRegistro { get; set; }
    public int IdUsuario { get; set; }

    public DateTime FechaRegistro {get; set;}
    public Usuario? Usuario { get; set; } // Relación con Usuario

        public ICollection<DetalleRegistroConsumo> DetalleRegistroConsumos { get; set; } = new List<DetalleRegistroConsumo>();

}
