using ComercioCore.Application.DTOs.Comerciante;
using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Application.DTOs.Establecimiento
{
    public class EstablecimientoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre del establecimiento es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string NombreEstablecimiento { get; set; }

        [Required(ErrorMessage = "Los ingresos son obligatorios")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Debe ser mayor a 0")]
        public decimal Ingresos { get; set; }

        [Required(ErrorMessage = "Número de empleados es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "Mínimo 1 empleado")]
        public int NumeroEmpleados { get; set; }

        [Required(ErrorMessage = "Comerciante es obligatorio")]
        public int ComercianteId { get; set; }
    }
    public class EstablecimientoDetailDto : EstablecimientoDto
    {
        public ComercianteDto Comerciante { get; set; }
    }
}
