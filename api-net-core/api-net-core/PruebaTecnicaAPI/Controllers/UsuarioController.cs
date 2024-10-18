using Core.DTO;
using Core.Entities;
using Infrastructure.Queries.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnicaAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioQuery _IUsuarioQuery;

        public UsuarioController(IUsuarioQuery IUsuarioQuery)
        {
            _IUsuarioQuery = IUsuarioQuery;
        }


        [HttpGet]
        public async Task<ActionResult> Listar()
        {
            try
            {
                var lista = await _IUsuarioQuery.ListarUsuarios();
                return Ok(new
                {
                    data = lista,
                    message = "",
                    status = StatusCodes.Status200OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    data = new { },
                    message = ex.Message,
                    status = StatusCodes.Status400BadRequest
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult> Crear(Usuario model)
        {

            try
            {
                UsuarioDTO Usuario = await _IUsuarioQuery.CrearUsuario(model);
                return Ok(new
                {
                    data = Usuario,
                    message = "",
                    status = StatusCodes.Status201Created
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    data = new { },
                    message = ex.Message,
                    status = StatusCodes.Status400BadRequest
                });
            }
        }


        [HttpPut]
        public async Task<ActionResult> Modificar(Usuario model)
        {

            try
            {
                UsuarioDTO Usuario = await _IUsuarioQuery.ActualizarUsuario(model);
                return Ok(new
                {
                    data = Usuario,
                    message = "",
                    status = StatusCodes.Status200OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    data = new { },
                    message = ex.Message,
                    status = StatusCodes.Status400BadRequest
                });
            }
        }


        [HttpDelete("idUsuario")]
        public async Task<ActionResult> Eliminar(int idUsuario)
        {

            try
            {
                Boolean exito = await _IUsuarioQuery.EliminarUsuario(idUsuario);
                return Ok(new
                {
                    data = exito,
                    message = "",
                    status = StatusCodes.Status200OK
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    data = new { },
                    message = ex.Message,
                    status = StatusCodes.Status400BadRequest
                });
            }
        }



    }
}
