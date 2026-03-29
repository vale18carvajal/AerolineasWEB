

using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public interface IPropietarioRepository
    {
        Task<IEnumerable<Propietario>> obtenerPropietariosActivosAsync();
        Task<Propietario> obtenerPorIdAsync(int id);
        Task<Propietario> obtenerPorIdentificacionPropietarioAsync(string identificacion);
        Task<IEnumerable<Propietario>> obtenerPorNombreAsync(string nombre);
        Task crearAsync(Propietario propietario);
        Task editarAsync(Propietario propietario);
        Task desactivarAsync(int id);
    }
}
