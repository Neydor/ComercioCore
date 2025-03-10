using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioCore.Application.DTOs.Comerciante
{
    public class ComercianteCreateDto
    {
        [Required]
        [StringLength(100)]
        public string NombreRazonSocial { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MunicipioId { get; set; }

        [Phone]
        [StringLength(20)]
        public string Telefono { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string CorreoElectronico { get; set; }

        [Required]
        [RegularExpression("^(Activo|Inactivo)$",
            ErrorMessage = "Estado debe ser 'Activo' o 'Inactivo'")]
        public string Estado { get; set; }
    }
}
