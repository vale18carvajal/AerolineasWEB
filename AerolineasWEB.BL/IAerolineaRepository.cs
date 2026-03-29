
using AerolineasWEB.Model;
namespace AerolineasWEB.BL
{
    public interface IAerolineaRepository
    {
        Task<IEnumerable<Aerolinea>> obtenerAerolineasActivasAsync();
        Task<Aerolinea> obtenerPorIdAsync(int id);
        Task<Aerolinea> obtenerPorIataAsync(string codigo_iata);
        Task<Aerolinea> obtenerPorNombreAsync(string nombre);
        Task<Aerolinea> obtenerPorTelefonoAsync(string telefono);
        Task crearAsync(Aerolinea aerolinea);
        Task editarAsync(Aerolinea aerolinea);
        Task desactivarAsync(int id);
    }
}
