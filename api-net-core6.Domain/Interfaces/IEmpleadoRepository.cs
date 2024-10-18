using api_net_core6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net_core6.Domain.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<Empleado> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Empleado>> ObtenerTodosAsync();
        Task AgregarAsync(Empleado empleado);
        Task ActualizarAsync(Empleado empleado);
        Task EliminarAsync(int id);
    }
}
