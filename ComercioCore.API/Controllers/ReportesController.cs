using ComercioCore.Application.DTOs.Reportes;
using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Text;

namespace ComercioCore.API.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IComercianteService _comercianteService;

        public ReportesController(IComercianteService comercianteService)
        {
            _comercianteService = comercianteService;
        }

        [HttpGet("comerciantes-activos")]
        public async Task<IActionResult> GenerarReporteComerciantesActivos()
        {
            var comerciantes = await _comercianteService.ObtenerComerciantesActivosConEstadisticas();

            var csvContent = GenerarCsv(comerciantes);

            return File(
                Encoding.UTF8.GetBytes(csvContent), 
                "text/csv; charset=utf-8", 
                $"comerciantes-activos-{DateTime.UtcNow.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture)}.csv"
                );
        }

        private string GenerarCsv(IEnumerable<ReporteComercianteActivoSP> comerciantes)
        {
            var csv = new StringBuilder();

            // Encabezados
            csv.AppendLine("Nombre o razón social|Municipio|Teléfono|Correo Electrónico|Fecha de Registro|Estado|Cantidad de Establecimientos|Total Ingresos|Cantidad de Empleados");

            // Datos
            foreach (var c in comerciantes)
            {
                //RazonSocial	Municipio	Telefono	CorreoElectronico	FechaRegistro	Estado	CantidadEstablecimientos	TotalIngresos	CantidadEmpleados
                
                csv.AppendLine(
                    $"{EscapePipe(c.RazonSocial)}|" +
                    $"{EscapePipe(c.Municipio)}|" +
                    $"{EscapePipe(c.Telefono ?? "N/A")}|" +
                    $"{EscapePipe(c.CorreoElectronico ?? "N/A")}|" +
                    $"{c.FechaRegistro:yyyy-MM-dd}|" +
                    $"{EscapePipe(c.Estado)}|" +
                    $"{c.CantidadEstablecimientos}|" +
                    $"{c.TotalIngresos:N2}|" +
                    $"{c.CantidadEmpleados}");
            }

            return csv.ToString();
        }

        private string EscapePipe(string value)
        {
            return value?.Replace("|", "\\|") ?? string.Empty;
        }
    }
}
