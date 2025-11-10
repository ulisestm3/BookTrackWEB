using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL
{
    [Table("vEmpleadosDetalle")]
    public class vEmpleadosDetalle
    {
        public int IdEmpleado { get; set; }
        public string? NombreCompleto { get; set; }
        public string? NumeroCedula { get; set; }
        public string? NumeroInss { get; set; }
        public string? NombreGenero { get; set; }
        public string? Direccion { get; set; }
        public string? NombreCargo { get; set; }
        public string? NombreHorario { get; set; }
        public string? DescripcionHorario { get; set; }
        public string? NombreArea { get; set; }
        public byte[]? Foto { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string? Telefono { get; set; }
        public string? ContactoEmergencia { get; set; }
        public bool Activo { get; set; }
    }
}
