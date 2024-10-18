using api_net_core6.Domain.Entities;
using api_net_core6.Domain.Interfaces;
using api_net_core6.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net_core6.Infrastructure.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly string _connectionString;

        public EmpleadoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Empleado> ObtenerPorIdAsync(int id)
        {
            Empleado empleado = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_ObtenerEmpleadoPorID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Empleado", id);

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            empleado = MapearEmpleado(reader);
                        }
                    }
                }
            }

            return empleado;
        }

        public async Task<IEnumerable<Empleado>> ObtenerTodosAsync()
        {
            var empleados = new List<Empleado>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_ObtenerEmpleados", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            empleados.Add(MapearEmpleadoTodos(reader));
                        }
                    }
                }
            }

            return empleados;
        }

        public async Task AgregarAsync(Empleado empleado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_InsertarEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                    command.Parameters.AddWithValue("@Fecha_Nacimiento", empleado.Fecha_Nacimiento);
                    command.Parameters.AddWithValue("@Fecha_Ingreso", empleado.Fecha_Ingreso);
                    command.Parameters.AddWithValue("@ID_AFP", empleado.Id_AFP);
                    command.Parameters.AddWithValue("@ID_Cargo", empleado.Id_Cargo);
                    command.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task ActualizarAsync(Empleado empleado)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_ActualizarEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ID_Empleado", empleado.Id);
                    command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                    command.Parameters.AddWithValue("@Fecha_Nacimiento", empleado.Fecha_Nacimiento);
                    command.Parameters.AddWithValue("@Fecha_Ingreso", empleado.Fecha_Ingreso);
                    command.Parameters.AddWithValue("@ID_AFP", empleado.Id_AFP);
                    command.Parameters.AddWithValue("@ID_Cargo", empleado.Id_Cargo);
                    command.Parameters.AddWithValue("@Sueldo", empleado.Sueldo);
                    command.Parameters.AddWithValue("@Status", empleado.Status);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EliminarAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("SP_EliminarEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Empleado", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private Empleado MapearEmpleado(IDataReader reader)
        {
            return new Empleado
            {
                Id = (int)reader["ID_Empleado"],
                Nombres = reader["Nombres"].ToString(),
                Apellidos = reader["Apellidos"].ToString(),
                Fecha_Nacimiento = (DateTime)reader["Fecha_Nacimiento"],
                Fecha_Ingreso = (DateTime)reader["Fecha_Ingreso"],
                Id_AFP = (int)reader["ID_AFP"],
                Id_Cargo = (int)reader["ID_Cargo"],
                Sueldo = (decimal)reader["Sueldo"],
                Status = (bool)reader["Status"]
            };
        }

        private Empleado MapearEmpleadoTodos(IDataReader reader)
        {
            return new Empleado
            {
                Id = (int)reader["ID_Empleado"],
                Nombres = reader["Nombres"].ToString(),
                Apellidos = reader["Apellidos"].ToString(),
                Fecha_Nacimiento = (DateTime)reader["Fecha_Nacimiento"],
                Fecha_Ingreso = (DateTime)reader["Fecha_Ingreso"],
                Id_AFP = (int)reader["ID_AFP"],
                AFP = reader["AFP"].ToString(),
                Id_Cargo = (int)reader["ID_Cargo"],
                Cargo = reader["Cargo"].ToString(),
                Sueldo = (decimal)reader["Sueldo"],
                Status = (bool)reader["Status"]
            };
        }
    }
}