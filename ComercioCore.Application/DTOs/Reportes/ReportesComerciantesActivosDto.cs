
namespace ComercioCore.Application.DTOs.Reportes
{
    public class ReportesComerciantesActivosDto
    {
        public string RazonSocial { get; set; }
        public string Municipio { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }
        public int CantidadEstablecimientos { get; set; }
        public decimal TotalIngresos { get; set; }
        public int CantidadEmpleados { get; set; }
    }
}
