

using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public interface IPropietarioRepository
    {
        Task<IEnumerable<Propietario>> obtenerPropietariosActivosAsync();
        Task<Propietario> obtenerPorIdAsync(int id);
        Task<Propietario> obtenerPorIdentificacionNombrePropietarioAsync(string identificacion);
        Task<Propietario> obtenerPorNombreAsync(string nombre);
        Task crearAsync(Propietario propietario);
        Task editarAsync(Propietario propietario);
        Task desactivarAsync(int id);
    }
}
