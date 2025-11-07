using System;
using System.ComponentModel.DataAnnotations;

namespace ExamenSegundoP.Models
{
    public class Participante
    {
        [Key] // <- Esto marca la columna Id como la clave primaria
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Twitter { get; set; }

        [MaxLength(100)]
        public string? Ocupacion { get; set; }

        [MaxLength(300)]
        public string? AvatarUrl { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
