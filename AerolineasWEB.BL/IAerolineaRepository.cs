
using AerolineasWEB.Model;
namespace AerolineasWEB.BL
{
    public interface IAerolineaRepository
    {
        Task<IEnumerable<Aerolinea>> obtenerAerolineasActivasAsync();
        Task<Aerolinea> obtenerPorIdAsync(int id);
        Task<IEnumerable<Aerolinea>> obtenerPorIataAsync(string codigo_iata);
        Task<Aerolinea> obtenerPorIataExactoAsync(string codigo_iata);
        Task<IEnumerable<Aerolinea>> obtenerPorNombreAsync(string nombre);
        Task<IEnumerable<Aerolinea>> obtenerPorTelefonoAsync(string telefono);
        Task crearAsync(Aerolinea aerolinea);
        Task editarAsync(Aerolinea aerolinea);
        Task desactivarAsync(int id);
    }
}
