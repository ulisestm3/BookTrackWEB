using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("Prestamos")]
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        [Required]
        public int SocioId { get; set; }
        [Required]
        public int EjemplarId { get; set; }
        [Required]
        public DateTime FechaSalida { get; set; }
        [Required]
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaDevolucion { get; set; }

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