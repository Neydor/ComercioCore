
using System.ComponentModel.DataAnnotations;

namespace ComercioCore.Application.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        [AllowedValues("Administrador", "Auxiliar de Registro",
            ErrorMessage = "Rol inválido")]
        public string Rol { get; set; }

    }
    public class UsuarioCreateDto : UsuarioDto
    {
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Debe tener entre 6 y 50 caracteres")]
        public string Contrasena { get; set; }
    }
}
