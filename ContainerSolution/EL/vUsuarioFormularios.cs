using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("vUsuarioFormularios")]
	public class vUsuarioFormularios
	{
		[Key]
		public long? Id { get; set; }
		[Required]
		public int IdUsuarioFormulario { get; set; }
		[Required]
		public byte IdFormulario { get; set; }
		[MaxLength(50)]
		[Required]
		public string Formulario { get; set; }
		[Required]
		public short IdUsuario { get; set; }
		public bool? TienePermiso { get; set; }
	}
}
