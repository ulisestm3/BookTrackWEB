using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
	[Table("Reportes")]
	public class Reportes
	{
		[Key]
		public byte IdReporte { get; set; }
		[MaxLength(50)]
		[Required]
		public string Reporte { get; set; }
		[MaxLength(200)]
		[Required]
		public string Observaciones { get; set; }
		[MaxLength(100)]
		public string ReportName { get; set; }
	}
}
