using ComercioCore.Application.DTOs.Establecimiento;
using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Application.DTOs.Comerciante
{
    public class ComercianteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre/Razón social es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string NombreRazonSocial { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un municipio válido")]
        public int MunicipioId { get; set; }
        public string MunicipioNombre { get; set; }

        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Estado es obligatorio")]
        [RegularExpression("^(Activo|Inactivo)$",
            ErrorMessage = "Estado debe ser 'Activo' o 'Inactivo'")]
        public string Estado { get; set; }

        public List<EstablecimientoDto> Establecimientos { get; set; } = new();
    }
}
