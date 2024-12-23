namespace MvcMovie.Models{

public class DatosSaludUsuario
{
    public int IdUsuario { get; set; } // Clave primaria y FK hacia Usuario
    public int Edad { get; set; }
    public Decimal Peso { get; set; }
    public required char Alergias { get; set; }
    public required string Enfermedades { get; set; }
    public DateTime FechaActualizacion { get; set; }

    // Relación uno a uno con Usuario
    public Usuario? Usuario { get; set; }
}
}