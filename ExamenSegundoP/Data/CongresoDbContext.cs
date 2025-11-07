using ExamenSegundoP.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenSegundoP.Data
{
    public class CongresoDbContext : DbContext
    {
        public CongresoDbContext(DbContextOptions<CongresoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Participante> Participantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración opcional por si PostgreSQL es sensible a nombres
            modelBuilder.Entity<Participante>().ToTable("participantes");
            modelBuilder.Entity<Participante>().Property(p => p.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
