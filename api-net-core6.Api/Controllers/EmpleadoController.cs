using api_net_core6.Application.DTOs;
using api_net_core6.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_net_core6.Api.Controllers
{
    [Route("api/empleado")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        // GET: api/empleado
        [HttpGet]
        public async Task<IActionResult> GetEmpleados()
        {
            try
            {
                var empleados = await _empleadoService.ObtenerTodosEmpleadosAsync();
                return Ok(new
                {
                    data = empleados,
                    error = (string)null,
                    status = 200
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    data = (string)null,
                    error = ex.Message,
                    status = 500
                });
            }
        }

        // GET: api/empleado/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpleado(int id)
        {
            try
            {
                var empleado = await _empleadoService.ObtenerEmpleadoPorIdAsync(id);
                if (empleado == null)
                {
                    return NotFound(new
                    {
                        data = new{},
                        error = "Empleado no encontrado",
                        status = 404
                    });
                }

                return Ok(new
                {
                    data = empleado,
                    error = (string)null,
                    status = 200
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    data = new { },
                    error = ex.Message,
                    status = 500
                });
            }
        }

        // POST: api/empleado
        [HttpPost]
        public async Task<IActionResult> PostEmpleado(EmpleadoDTO empleadoDto)
        {
            try
            {
                await _empleadoService.CrearEmpleadoAsync(empleadoDto);
                return CreatedAtAction(nameof(GetEmpleado), new { id = empleadoDto.Id }, new
                {
                    data = empleadoDto,
                    error = (string)null,
                    status = 201
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    data = new{},
                    error = ex.Message,
                    status = 500
                });
            }
        }

        // PUT: api/empleado/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, EmpleadoDTO empleadoDto)
        {
            if (id != empleadoDto.Id)
            {
                return BadRequest(new
                {
                    data = new{},
                    error = "ID del empleado no coincide",
                    status = 400
                });
            }

            try
            {
                await _empleadoService.ActualizarEmpleadoAsync(empleadoDto);
                return Ok(new
                {
                    data = (string)null,
                    error = (string)null,
                    status = 200
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    data = new{},
                    error = ex.Message,
                    status = 500
                });
            }
        }

        // DELETE: api/empleado/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            try
            {
                await _empleadoService.EliminarEmpleadoAsync(id);
                return Ok(new
                {
                    data = (string)null,
                    error = (string)null,
                    status = 200
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new
                {
                    data = new{},
                    error = ex.Message,
                    status = 500
                });
            }
        }
    }
}