
using AerolineasWEB.BL;
using AerolineasWEB.Model;
using Microsoft.EntityFrameworkCore;

namespace AerolineasWEB.DA
{
    public class AvionRepository : IAvionRepository
    {
        private readonly DBContexto _context;

        public AvionRepository (DBContexto context)
        {
            _context = context;
        }
        public async Task crearAsync(Avion avion)
        {
            await _context.Avion.AddAsync(avion);
            await _context.SaveChangesAsync();
        }

        public async Task desactivarAsync(int id)
        {
            Avion avion = await obtenerPorIdAsync(id);
            if (avion != null) 
            {
                avion.estado = EstadoAvion.Inactivo;
                _context.Avion.Update(avion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task editarAsync(Avion avion)
        {
            _context.Avion.Update(avion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Avion>> obtenerAvionesActivosAsync()
        {
            return await _context.Avion.Where(a => a.estado == EstadoAvion.Activo).ToArrayAsync();
        }

        public async Task<Avion> obtenerPorIdAsync(int id)
        {
            return await _context.Avion.FirstOrDefaultAsync(a => a.id_avion == id);
        }
        public async Task<Avion> obtenerPorMatriculaAsync(string matricula)
        {
            return await _context.Avion.FirstOrDefaultAsync(a => a.matricula == matricula);
        }

        public async Task<IEnumerable<Avion>> obtenerPorNombreAerolineaAsync(string nombreAerolinea)
        {
            return await _context.Avion.Include(a => a.aerolinea).Where(a => a.aerolinea.nombre.Contains(nombreAerolinea) && a.estado == EstadoAvion.Activo).ToArrayAsync();
        }

        public async Task<IEnumerable<Avion>> obtenerPorNombrePropietarioAsync(string nombrePropietario)
        {
            return await _context.Avion.Include(a => a.aerolinea).Where(a => a.propietario.nombre.Contains(nombrePropietario) && a.estado == EstadoAvion.Activo).ToArrayAsync();
        }
        public async Task<bool> ExistenAvionesActivosPorAerolinea(int id_aerolinea){
            return await _context.Avion.AnyAsync(a => a.id_aerolinea == id_aerolinea && a.estado == EstadoAvion.Activo);
        }
        public async Task<bool> ExistenAvionesActivosPorPropietario(int id_propietario)
        {
            return await _context.Avion.AnyAsync(a => a.id_propietario == id_propietario && a.estado == EstadoAvion.Activo);
        }
    }
}
