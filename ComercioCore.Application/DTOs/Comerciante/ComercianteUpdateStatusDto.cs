using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Application.DTOs.Comerciante
{
    public class ComercianteUpdateStatusDto
    {
        [Required]
        [RegularExpression("^(Activo|Inactivo)$",
            ErrorMessage = "Estado debe ser 'Activo' o 'Inactivo'")]
        public string Estado { get; set; }
    }
}
