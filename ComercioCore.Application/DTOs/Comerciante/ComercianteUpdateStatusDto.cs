using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Application.DTOs.Comerciante
{
    public class UpdateEstadoDto
    {
        [Required]
        [RegularExpression("^(Activo|Inactivo)$")]
        public string Estado { get; set; }
    }
}
