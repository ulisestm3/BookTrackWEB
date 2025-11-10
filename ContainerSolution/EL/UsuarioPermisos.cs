using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("UsuarioPermisos")]
	public class UsuarioPermisos
	{
		[Key]
		public int IdUsuarioPermiso { get; set; }
		[Required]
		public short IdUsuario { get; set; }
		[Required]
		public short IdPermisoFormulario { get; set; }
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
