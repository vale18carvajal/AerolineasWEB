using AerolineasWEB.BL;
using AerolineasWEB.Model;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AerolineasWEB.DA
{
    public class AerolineaRepository : IAerolineaRepository
    {
        private readonly DBContexto _context;

        public AerolineaRepository (DBContexto context)
        {
            _context = context;
        }

        public async Task crearAsync(Aerolinea aerolinea)
        {
            await _context.Aerolinea.AddAsync(aerolinea);
            await _context.SaveChangesAsync();
        }

        public async Task desactivarAsync(int id) //Se usa para borrado lógico
        {
            Aerolinea aerolinea = await obtenerPorIdAsync(id);
            if(aerolinea != null)
            {
                aerolinea.estado = EstadoAerolinea.Inactivo;
                _context.Aerolinea.Update(aerolinea);
                await _context.SaveChangesAsync();
            }
        }

        public async Task editarAsync(Aerolinea aerolinea)
        {
            _context.Aerolinea.Update(aerolinea);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aerolinea>> obtenerAerolineasActivasAsync()
        {
            return await _context.Aerolinea.Where(a => a.estado == EstadoAerolinea.Activo).ToListAsync();
        }

        public async Task<IEnumerable<Aerolinea>> obtenerPorIataAsync(string codigo_iata)
        {
            //return await _context.Aerolinea.FirstOrDefaultAsync(a => a.codigo_iata == codigo_iata && a.estado == EstadoAerolinea.Activo);
            return await _context.Aerolinea.Where(a => a.codigo_iata.Contains(codigo_iata) && a.estado == EstadoAerolinea.Activo).ToListAsync();
        }
        public async Task<Aerolinea> obtenerPorIataExactoAsync(string codigo_iata)
        {
            return await _context.Aerolinea.FirstOrDefaultAsync(a => a.codigo_iata == codigo_iata);
        }

        public async Task<Aerolinea> obtenerPorIdAsync(int id)
        {
            return await _context.Aerolinea.FirstOrDefaultAsync(a => a.id_aerolinea == id);
        }

        public async Task<IEnumerable<Aerolinea>> obtenerPorNombreAsync(string nombre)
        {
            return await _context.Aerolinea.Where(a => a.nombre.Contains(nombre) && a.estado == EstadoAerolinea.Activo).ToListAsync();
        }

        public async Task<IEnumerable<Aerolinea>> obtenerPorTelefonoAsync(string telefono)
        {
            //return await _context.Aerolinea.FirstOrDefaultAsync(a => a.telefono == telefono && a.estado == EstadoAerolinea.Activo);
            return await _context.Aerolinea.Where(a => a.telefono.Contains(telefono) && a.estado == EstadoAerolinea.Activo).ToListAsync();
        }
    }
}
