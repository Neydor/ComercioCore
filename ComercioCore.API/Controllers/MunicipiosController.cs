using ComercioCore.Application.Common;
using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Auxiliar de Registro")]
    public class MunicipiosController : ControllerBase
    {
        private readonly IMunicipioService _municipioService;

        public MunicipiosController(IMunicipioService municipioService)
        {
            _municipioService = municipioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMunicipios()
        {
            var result = await _municipioService.ObtenerTodos();
            return Ok(ApiResponse<IEnumerable<Municipio>>.SuccessResult(result));
        }
    }
}
