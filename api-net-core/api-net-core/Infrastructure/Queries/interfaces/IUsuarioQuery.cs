using Core.DTO;
using Core.Entities;

namespace Infrastructure.Queries.interfaces
{
    public interface IUsuarioQuery
    {

        Task<List<UsuarioDTO>> ListarUsuarios();
        Task<UsuarioDTO> CrearUsuario(Usuario usuario);
        Task<UsuarioDTO> ActualizarUsuario(Usuario usuario);
        Task<bool> EliminarUsuario(int idUsuario);

    }
}
