using api_net_core6.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net_core6.Application.Interfaces
{
    public interface IEmpleadoService
    {
        Task<EmpleadoDTO> ObtenerEmpleadoPorIdAsync(int id);
        Task<IEnumerable<EmpleadoDTO>> ObtenerTodosEmpleadosAsync();
        Task CrearEmpleadoAsync(EmpleadoDTO empleadoDto);
        Task ActualizarEmpleadoAsync(EmpleadoDTO empleadoDto);
        Task EliminarEmpleadoAsync(int id);
    }
}
