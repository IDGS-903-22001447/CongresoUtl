namespace ExamenSegundoP.Models
{
    public class Participante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Twitter { get; set; } = null!;
        public string Ocupacion { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
