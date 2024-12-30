
namespace MvcMovie.Models{
public class Usuario
{
    public int IdUsuario { get; set; } // Clave primaria
    public required string Correo { get; set; }
    public required string? Contrasena { get; set; }

    public required string UsuarioNombre { get; set; }
    // Relación uno a uno con DatosSaludUsuario
    public DatosSaludUsuario? DatosSaludUsuario { get; set; }
        public ICollection<Registro> Registros { get; set; } = new List<Registro>();

}
}