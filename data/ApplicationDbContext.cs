using Microsoft.EntityFrameworkCore;
using MvcMovie.Models; // Asegúrate de importar el namespace correcto


namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet para cada tabla
        public required DbSet<Usuario> Usuarios { get; set; }
        public required DbSet<DatosSaludUsuario> Salud { get; set; }
        public required DbSet<CategoriaIngrediente> CategoriasIngredientes { get; set; }
        public required DbSet<Ingrediente> Ingredientes { get; set; }
        public required DbSet<Platillo> Platillos { get; set; }
        public required DbSet<IngredientePlatillo> IngredientesPlatillos { get; set; }
        public required DbSet<DetalleRegistroConsumo> DetalleRegistroConsumos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<DatosSaludUsuario>().ToTable("Salud"); // Nombre de la tabla en la base de datos


            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.DatosSaludUsuario)
                .WithOne(d => d.Usuario)
                .HasForeignKey<DatosSaludUsuario>(d => d.IdUsuario);

            modelBuilder.Entity<Usuario>().ToTable("Usuarios"); // Nombre de la tabla en la base de datos


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

            // Configuración de Platillos
            modelBuilder.Entity<Platillo>()
                .HasKey(p => p.IdPlatillo);

            // Configuración de IngredientesPlatillos (Muchos a Muchos)
            modelBuilder.Entity<IngredientePlatillo>()
                .HasKey(ip => ip.IdIngredientePlatillo);

            modelBuilder.Entity<IngredientePlatillo>()
                .HasIndex(ip => new { ip.IdPlatillo, ip.IdIngrediente })
                .IsUnique();

            modelBuilder.Entity<IngredientePlatillo>()
                .HasOne(ip => ip.Platillo)
                .WithMany(p => p.IngredientesPlatillos)
                .HasForeignKey(ip => ip.IdPlatillo);

            modelBuilder.Entity<IngredientePlatillo>()
                .HasOne(ip => ip.Ingrediente)
                .WithMany(i => i.IngredientesPlatillos)
                .HasForeignKey(ip => ip.IdIngrediente);

            // Configuración de Registros
            modelBuilder.Entity<Registro>()
                .HasKey(r => r.IdRegistro);

            modelBuilder.Entity<Registro>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Registros)
                .HasForeignKey(r => r.IdUsuario);

            // Configuración de DetalleRegistroConsumo
            modelBuilder.Entity<DetalleRegistroConsumo>()
                .HasKey(drc => drc.IdDetalle);

            modelBuilder.Entity<DetalleRegistroConsumo>()
                .HasOne(drc => drc.Registro)
                .WithMany(r => r.DetalleRegistroConsumos)
                .HasForeignKey(drc => drc.IdRegistro);

            modelBuilder.Entity<DetalleRegistroConsumo>()
                .HasOne(drc => drc.CategoriaIngrediente)
                .WithMany(ci => ci.DetalleRegistroConsumos)
                .HasForeignKey(drc => drc.IdCategoria);
        }
    }
}
