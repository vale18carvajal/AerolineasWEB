
using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public interface IAdministradorAerolinea
    {
        Task<IEnumerable<Aerolinea>> ObtenerListaAerolineasActivasAsync();
        Task<Aerolinea> ObtenerAerolineaAsync(int id);
        Task<Aerolinea> ObtenerPorIataAsync(string codigo_iata);
        Task<IEnumerable<Aerolinea>> ObtenerPorNombreAsync(string nombre);
        Task<Aerolinea> ObtenerPorTelefonoAsync(string telefono);
        Task RegistrarAerolineaAsync(Aerolinea aerolinea);
        Task EditarAerolineaAsync(Aerolinea aerolinea);
        Task DesactivarAerolineaAsync(int id);
    }
}
