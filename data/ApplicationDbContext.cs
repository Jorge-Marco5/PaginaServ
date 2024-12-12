using Microsoft.EntityFrameworkCore;
using Usuario.Models; // Asegúrate de que el espacio de nombres sea correcto


namespace ApplicationDbContext.Data
{
    public class ApplicationDbContext  : DbContext
    {
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } // Define la entidad Usuario
    }
}
