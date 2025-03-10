using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ComercioCore.Application.DTOs.Resquests
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [DefaultValue("admin@comercio.com")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Debe tener entre 6 y 50 caracteres")]
        [DefaultValue("Admin123")]
        public string Contrasena { get; set; }
    }
}
