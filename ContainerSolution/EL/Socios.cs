using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Socios")]
    public class Socios
    {
        [Key]
        public int SocioId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Socio { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        [MaxLength(12)]
        public string? Telefono { get; set; }

        [Required]
        public bool Activo { get; set; }

        [Required]
        public int UsuarioRegistra { get; set; }

        [Required]
        public DateTime FechaRegistro { get; set; }

        public int? UsuarioActualiza { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}