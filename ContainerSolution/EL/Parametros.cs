using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("Parametros")]
	public class Parametros
	{
		[Key]
		public byte IdParametro { get; set; }
		[MaxLength(200)]
		[Required]
		public string Descripcion { get; set; }
		[MaxLength(100)]
		[Required]
		public string Valor { get; set; }
		[MaxLength(50)]
		[Required]
		public string TipoDato { get; set; }
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
