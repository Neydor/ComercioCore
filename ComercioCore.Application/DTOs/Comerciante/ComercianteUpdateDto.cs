using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Application.DTOs.Comerciante
{
    public class ComercianteUpdateDto : ComercianteCreateDto
    {
        [Required]
        public DateTime FechaRegistro { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "El teléfono solo puede contener dígitos.")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public new string? Telefono { get; set; }
    }
}
