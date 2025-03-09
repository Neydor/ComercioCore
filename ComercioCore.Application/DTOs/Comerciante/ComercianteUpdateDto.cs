using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Application.DTOs.Comerciante
{
    public class ComercianteUpdateDto : ComercianteCreateDto
    {
        [Required]
        public DateTime FechaRegistro { get; set; }
    }
}
