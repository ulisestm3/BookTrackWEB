using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("UsuarioReportes")]
	public class UsuarioReportes
	{
		[Key]
		public byte IdUsuarioReporte { get; set; }
		[Required]
		public short IdUsuario { get; set; }
		[Required]
		public byte IdReporte { get; set; }
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
