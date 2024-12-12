public class Usuario
{
    public int IdUsuario { get; set; } // Clave primaria
    public required string Correo { get; set; }
    public required string Contrasena { get; set; }

    // Relación uno a uno con DatosSaludUsuario
    public required DatosSaludUsuario DatosSaludUsuario { get; set; }
}
