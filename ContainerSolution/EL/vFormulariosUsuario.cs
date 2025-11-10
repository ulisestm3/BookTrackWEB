using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("vFormulariosUsuario")]
	public class vFormulariosUsuario
	{
		[Key]
		[Required]
		public int IdUsuarioFormulario { get; set; }
		[Required]
		public short IdUsuario { get; set; }
		[MaxLength(20)]
		[Required]
		public string Login { get; set; }
		[Required]
		public byte IdFormulario { get; set; }
		[MaxLength(50)]
		[Required]
		public string Formulario { get; set; }
	}
}
