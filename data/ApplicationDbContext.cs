using Microsoft.EntityFrameworkCore;
namespace MvcMovie.Data{

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSet para cada tabla
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<DatosSaludUsuario> DatosSaludUsuarios { get; set; }
    public DbSet<CategoriaIngrediente> CategoriasIngredientes { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Usuario
        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.IdUsuario);

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.DatosSaludUsuario)
            .WithOne(d => d.Usuario)
            .HasForeignKey<DatosSaludUsuario>(d => d.IdUsuario);

        // Configuración de DatosSaludUsuario
        modelBuilder.Entity<DatosSaludUsuario>()
            .HasKey(d => d.IdUsuario);

        // Configuración de CategoriaIngrediente
        modelBuilder.Entity<CategoriaIngrediente>()
            .HasKey(c => c.IdCategoria);

        modelBuilder.Entity<CategoriaIngrediente>()
            .HasMany(c => c.Ingredientes)
            .WithOne(i => i.CategoriaIngrediente)
            .HasForeignKey(i => i.IdCategoria);

        // Configuración de Ingrediente
        modelBuilder.Entity<Ingrediente>()
            .HasKey(i => i.IdIngrediente);
    }
}
}