

using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public interface IAdministradorPropietario
    {
        Task<IEnumerable<Propietario>> ObtenerListaPropietariosActivosAsync();
        Task<Propietario> ObtenerPropietarioAsync(int id);
        Task<IEnumerable<Propietario>> ObtenerPorIdentificacionAsync(string identificacion);
        Task<Propietario> ObtenerPorIdentificacionExactaAsync(string identificacion);
        Task<IEnumerable<Propietario>> ObtenerPorNombreAsync(string nombre);
        Task RegistrarPropietarioAsync(Propietario propietario);
        Task EditarPropietarioAsync(Propietario propietario);
        Task DesactivarPropietarioAsync(int id);
    }
}
