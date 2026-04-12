
using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public interface IAdministradorAerolinea
    {
        Task<IEnumerable<Aerolinea>> ObtenerListaAerolineasActivasAsync();
        Task<Aerolinea> ObtenerAerolineaAsync(int id);
        Task<IEnumerable<Aerolinea>> ObtenerPorIataAsync(string codigo_iata);
        Task<Aerolinea> ObtenerPorIataExactoAsync(string codigo_iata);
        Task<IEnumerable<Aerolinea>> ObtenerPorNombreAsync(string nombre);
        Task<IEnumerable<Aerolinea>> ObtenerPorTelefonoAsync(string telefono);
        Task RegistrarAerolineaAsync(Aerolinea aerolinea);
        Task EditarAerolineaAsync(Aerolinea aerolinea);
        Task DesactivarAerolineaAsync(int id);
    }
}
