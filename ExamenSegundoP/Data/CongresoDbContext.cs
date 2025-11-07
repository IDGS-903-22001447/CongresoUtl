
using ExamenSegundoP.Models;

using Microsoft.EntityFrameworkCore;
namespace ExamenSegundoP.Data
{
    public class CongresoDbContext : DbContext
    {
        public CongresoDbContext(DbContextOptions<CongresoDbContext> options) : base(options) { }
        public DbSet<Participante> Participantes { get; set; } = null!;
    }
}
