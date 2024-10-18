using api_net_core6.Application.DTOs;
using api_net_core6.Application.Interfaces;
using api_net_core6.Domain.Entities;
using api_net_core6.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net_core6.Application.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;

        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public async Task<EmpleadoDTO> ObtenerEmpleadoPorIdAsync(int id)
        {
            var empleado = await _empleadoRepository.ObtenerPorIdAsync(id);
            return MapearEmpleadoDTO(empleado);
        }

        public async Task<IEnumerable<EmpleadoDTO>> ObtenerTodosEmpleadosAsync()
        {
            var empleados = await _empleadoRepository.ObtenerTodosAsync();
            var empleadosDto = new List<EmpleadoDTO>();

            foreach (var empleado in empleados)
            {
                empleadosDto.Add(MapearEmpleadoDTOTodos(empleado));
            }

            return empleadosDto;
        }

        public async Task CrearEmpleadoAsync(EmpleadoDTO empleadoDto)
        {
            var empleado = MapearEmpleado(empleadoDto);
            await _empleadoRepository.AgregarAsync(empleado);
        }

        public async Task ActualizarEmpleadoAsync(EmpleadoDTO empleadoDto)
        {
            var empleado = MapearEmpleado(empleadoDto);
            await _empleadoRepository.ActualizarAsync(empleado);
        }

        public async Task EliminarEmpleadoAsync(int id)
        {
            await _empleadoRepository.EliminarAsync(id);
        }

        private Empleado MapearEmpleado(EmpleadoDTO empleadoDto)
        {
            return new Empleado
            {
                Id = empleadoDto.Id,
                Nombres = empleadoDto.Nombres,
                Apellidos = empleadoDto.Apellidos,
                Fecha_Nacimiento = empleadoDto.FechaNacimiento,
                Fecha_Ingreso = empleadoDto.FechaIngreso,
                Id_AFP = empleadoDto.IdAFP,
                Id_Cargo = empleadoDto.IdCargo,
                Sueldo = empleadoDto.Sueldo,
                Status = empleadoDto.Status
            };
        }

        private EmpleadoDTO MapearEmpleadoDTO(Empleado empleado)
        {
            return new EmpleadoDTO
            {
                Id = empleado.Id,
                Nombres = empleado.Nombres,
                Apellidos = empleado.Apellidos,
                FechaNacimiento = empleado.Fecha_Nacimiento,
                FechaIngreso = empleado.Fecha_Ingreso,
                IdAFP = empleado.Id_AFP,
                IdCargo = empleado.Id_Cargo,
                Sueldo = empleado.Sueldo,
                Status = empleado.Status
            };
        }

        private EmpleadoDTO MapearEmpleadoDTOTodos(Empleado empleado)
        {
            return new EmpleadoDTO
            {
                Id = empleado.Id,
                Nombres = empleado.Nombres,
                Apellidos = empleado.Apellidos,
                FechaNacimiento = empleado.Fecha_Nacimiento,
                FechaIngreso = empleado.Fecha_Ingreso,
                IdAFP = empleado.Id_AFP,
                AFP = empleado.AFP,
                IdCargo = empleado.Id_Cargo,
                Cargo = empleado.Cargo,
                Sueldo = empleado.Sueldo,
                Status = empleado.Status
            };
        }
    }
}
