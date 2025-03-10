using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioCore.Application.DTOs.Reportes
{
    public class ReportesComerciantesActivosDto
    {
        public string NombreRazonSocial { get; set; }
        public string Municipio { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }
        public int CantidadEstablecimientos { get; set; }
        public decimal TotalIngresos { get; set; }
        public int TotalEmpleados { get; set; }
    }
}
