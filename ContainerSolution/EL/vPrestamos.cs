using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Keyless]
    [Table("vPrestamos")]
    public class vPrestamos
    {
        public int PrestamoId { get; set; }
        [Required]
        public int SocioId { get; set; }
        [MaxLength(150)]
        public string? Socio { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        [MaxLength(12)]
        public string? Telefono { get; set; }
        [Required]
        public int EjemplarId { get; set; }
        [MaxLength(50)]
        public string? CodigoBarra { get; set; }
        public int? PublicacionLibroId { get; set; }
        public int? LibroId { get; set; }
        [MaxLength(200)]
        public string? TituloLibro { get; set; }
        public int? AutorId { get; set; }
        [MaxLength(150)]
        public string? Autor { get; set; }
        public int? CategoriaId { get; set; }
        [MaxLength(100)]
        public string? Categoria { get; set; }
        [MaxLength(20)]
        public string? ISBN { get; set; }
        public int? AnioPublicacion { get; set; }
        public int? EstadoLibroId { get; set; }
        [MaxLength(50)]
        public string? EstadoLibro { get; set; }

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