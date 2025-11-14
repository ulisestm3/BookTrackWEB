using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Keyless]
    [Table("vEjemplares")]
    public class vEjemplares
    {
        public int EjemplarId { get; set; }

        [MaxLength(50)]
        public string? CodigoBarra { get; set; }

        public int PublicacionLibroId { get; set; }

        public int EstadoLibroId { get; set; }

        [MaxLength(50)]
        [Required]
        public string EstadoLibro { get; set; }

        public DateTime FechaAdquisicion { get; set; }

        [MaxLength(20)]
        public string? ISBN { get; set; }

        public int AnioPublicacion { get; set; }

        public int LibroId { get; set; }

        [MaxLength(200)]
        [Required]
        public string TituloLibro { get; set; }

        public int AutorId { get; set; }
        public int CategoriaId { get; set; }

        [MaxLength(150)]
        public string Autor { get; set; }

        [MaxLength(150)]
        public string Categoria { get; set; }

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