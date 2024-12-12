public class DatosSaludUsuario
{
    public int IdUsuario { get; set; } // Clave primaria y FK hacia Usuario
    public int Edad { get; set; }
    public int Peso { get; set; }
    public required string Alergias { get; set; }
    public required string Enfermedades { get; set; }
    public DateTime FechaActualizacion { get; set; }

    // Relación uno a uno con Usuario
    public required Usuario Usuario { get; set; }
}