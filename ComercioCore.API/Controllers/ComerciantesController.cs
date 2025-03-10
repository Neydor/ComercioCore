using Application.Common.Pagination;
using AutoMapper;
using ComercioCore.Application.Common;
using ComercioCore.Application.DTOs.Comerciante;
using ComercioCore.Application.DTOs.Comerciante.Pagination;
using ComercioCore.Application.Interfaces.Services;
using ComercioCore.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComercioCore.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ComerciantesController : ControllerBase
    {
        private readonly IComercianteService _comercianteService;
        private readonly IMapper _mapper;

        public ComerciantesController(IComercianteService comercianteService, IMapper mapper)
        {
            _comercianteService = comercianteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResponse<ComercianteDto>>>> GetAll(
            [FromQuery] ComercianteFilter filter)
        {
            var pagedResponse = await _comercianteService.GetPagedAsync(filter);
            return Ok(ApiResponse<PagedResponse<ComercianteDto>>.SuccessResult(pagedResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ComercianteDto>>> GetById(int id)
        {
            var comerciante = await _comercianteService.GetByIdAsync(id);
            return Ok(ApiResponse<ComercianteDto>.SuccessResult(_mapper.Map<ComercianteDto>(comerciante)));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ComercianteDto>>> Create(ComercianteCreateDto dto)
        {
            var entity = _mapper.Map<Comerciante>(dto);
            var created = await _comercianteService.CreateAsync(entity);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                ApiResponse<ComercianteDto>.SuccessResult(_mapper.Map<ComercianteDto>(created)
               )
             );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ComercianteDto>>> Update(int id, ComercianteUpdateDto dto)
        {
            var entity = _mapper.Map<Comerciante>(dto);
            var updated = await _comercianteService.UpdateAsync(id, entity);
            return Ok(ApiResponse<ComercianteDto>.SuccessResult(_mapper.Map<ComercianteDto>(updated)));
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            await _comercianteService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        public async Task<ActionResult<ApiResponse>> UpdateEstado(int id, ComercianteUpdateStatusDto dto)
        {
            await _comercianteService.UpdateEstadoAsync(id, dto.Estado);
            return Ok(ApiResponse.SuccessResult("Estado actualizado"));
        }
    }
}
