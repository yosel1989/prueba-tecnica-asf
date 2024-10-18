using Core.DTO;
using Core.Entities;
using Infrastructure.Queries.interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
namespace Infrastructure.Queries
{
    public class UsuarioQuery : IUsuarioQuery
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnectionString;
        public UsuarioQuery(IConfiguration configuration)
        {
            //Deberá modificar la cadena de conexión antes de ejecutar
            _configuration = configuration;
            ConnectionString = ConfigurationExtensions.GetConnectionString(this._configuration, "PruebaTecnicaDB"); ;
        }


        public async Task<List<UsuarioDTO>> ListarUsuarios()
        {
            try
            {
                using SqlConnection conn = new SqlConnection(ConnectionString);
                await conn.OpenAsync();
                using SqlCommand cmd = new SqlCommand("SP_Usuario_Listar", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                var reader = await cmd.ExecuteReaderAsync();
                var output = await ReadListar(reader);

                conn.Close();

                return output;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        public async Task<UsuarioDTO> CrearUsuario(Usuario alumno)
        {
            try
            {
                using SqlConnection conn = new(ConnectionString);
                await conn.OpenAsync();
                using SqlCommand cmd = new SqlCommand("SP_Usuario_Crear", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("pNombre", alumno.Name);
                cmd.Parameters.AddWithValue("pTelefono", alumno.Phone);
                cmd.Parameters.AddWithValue("pEmail", alumno.Email);
                cmd.Parameters.AddWithValue("pCompany", alumno.Company);
                cmd.Parameters.AddWithValue("pAddress", alumno.Address);
                cmd.Parameters.AddWithValue("pLatitude", alumno.Latitude);
                cmd.Parameters.AddWithValue("pLongitude", alumno.Longitude);
                var reader = await cmd.ExecuteReaderAsync();
                var output = await ReadCrear(reader);

                conn.Close();

                return output;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }


        public async Task<UsuarioDTO> ActualizarUsuario(Usuario alumno)
        {
            try
            {
                using SqlConnection conn = new(ConnectionString);
                await conn.OpenAsync();
                using SqlCommand cmd = new SqlCommand("SP_Usuario_Modificar", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("pId", alumno.Id);
                cmd.Parameters.AddWithValue("pNombre", alumno.Name);
                cmd.Parameters.AddWithValue("pTelefono", alumno.Phone);
                cmd.Parameters.AddWithValue("pEmail", alumno.Email);
                cmd.Parameters.AddWithValue("pCompany", alumno.Company);
                cmd.Parameters.AddWithValue("pAddress", alumno.Address);
                cmd.Parameters.AddWithValue("pLatitude", alumno.Latitude);
                cmd.Parameters.AddWithValue("pLongitude", alumno.Longitude);
                var reader = await cmd.ExecuteReaderAsync();
                var output = await ReadActualizar(reader);

                conn.Close();

                return output;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        public async Task<bool> EliminarUsuario(int idUsuario)
        {
            try
            {
                using SqlConnection conn = new(ConnectionString);
                await conn.OpenAsync();
                using SqlCommand cmd = new SqlCommand("SP_Usuario_Eliminar", conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("pId", idUsuario);
                var reader = await cmd.ExecuteReaderAsync();
                var output = await ReadEliminar(reader);

                conn.Close();

                return output;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        








        static async Task<List<UsuarioDTO>> ReadListar(DbDataReader reader)
        {
            try
            {
                List<UsuarioDTO> alumnos = new List<UsuarioDTO>();
                while (await reader.ReadAsync())
                {
                    UsuarioDTO alumno = new UsuarioDTO();
                    alumno.Id = Convert.ToInt32(reader["Id"]);
                    alumno.Name = Convert.ToString(reader["Name"]);
                    alumno.Phone = Convert.ToString(reader["Phone"]);
                    alumno.Email = Convert.ToString(reader["Email"]);
                    alumno.Company = Convert.ToString(reader["Company"]);
                    alumno.Address = Convert.ToString(reader["Address"]);
                    alumno.Latitude = Convert.ToDecimal(reader["Latitude"]);
                    alumno.Longitude = Convert.ToDecimal(reader["Longitude"]);

                    alumnos.Add(alumno);
                }

                return alumnos;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }


        static async Task<UsuarioDTO> ReadCrear(DbDataReader reader)
        {
            try
            {
                UsuarioDTO alumno = new UsuarioDTO();
                while (await reader.ReadAsync())
                {
                    alumno.Id = Convert.ToInt32(reader["Id"]);
                    alumno.Name = Convert.ToString(reader["Name"]);
                    alumno.Phone = Convert.ToString(reader["Phone"]);
                    alumno.Email = Convert.ToString(reader["Email"]);
                    alumno.Company = Convert.ToString(reader["Company"]);
                    alumno.Address = Convert.ToString(reader["Address"]);
                    alumno.Latitude = Convert.ToDecimal(reader["Latitude"]);
                    alumno.Longitude = Convert.ToDecimal(reader["Longitude"]);
                }

                return alumno;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }



        static async Task<UsuarioDTO> ReadActualizar(DbDataReader reader)
        {
            try
            {
                UsuarioDTO alumno = new UsuarioDTO();
                while (await reader.ReadAsync())
                {
                    alumno.Id = Convert.ToInt32(reader["Id"]);
                    alumno.Name = Convert.ToString(reader["Name"]);
                    alumno.Phone = Convert.ToString(reader["Phone"]);
                    alumno.Email = Convert.ToString(reader["Email"]);
                    alumno.Company = Convert.ToString(reader["Company"]);
                    alumno.Address = Convert.ToString(reader["Address"]);
                    alumno.Latitude = Convert.ToDecimal(reader["Latitude"]);
                    alumno.Longitude = Convert.ToDecimal(reader["Longitude"]);
                }

                return alumno;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        static async Task<bool> ReadEliminar(DbDataReader reader)
        {
            try
            {
                Boolean response = false;
                while (await reader.ReadAsync())
                {
                    response = Convert.ToBoolean(reader["Exito"]);
                }

                return response;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

    }
}
