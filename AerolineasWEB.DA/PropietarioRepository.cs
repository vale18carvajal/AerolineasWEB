

using AerolineasWEB.BL;
using AerolineasWEB.Model;
using Microsoft.EntityFrameworkCore;

namespace AerolineasWEB.DA
{
    public class PropietarioRepository : IPropietarioRepository
    {
        private readonly DBContexto _context;
        public PropietarioRepository (DBContexto context)
        {
            _context = context;
        }

        public async Task crearAsync(Propietario propietario)
        {
            await _context.Propietario.AddAsync(propietario);
            await _context.SaveChangesAsync();
        }

        public async Task desactivarAsync(int id)
        {
           Propietario propietario = await obtenerPorIdAsync(id);
            if (propietario != null)
            {
                propietario.estado = EstadoPropietario.Inactivo;
                _context.Propietario.Update(propietario);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task editarAsync(Propietario propietario)
        {
            _context.Propietario.Update(propietario);
            await _context.SaveChangesAsync();
        }

        public async Task<Propietario> obtenerPorIdAsync(int id)
        {
            return await _context.Propietario.FirstOrDefaultAsync(p => p.id_propietario == id);
        }

        public async Task<Propietario> obtenerPorIdentificacionPropietarioAsync(string identificacion)
        {
            return await _context.Propietario.FirstOrDefaultAsync(p => p.identificacion == identificacion);
        }

        public async Task<IEnumerable<Propietario>> obtenerPorNombreAsync(string nombre)
        {
            return await _context.Propietario.Where(p => p.nombre.Contains(nombre)).ToListAsync();
        }

        public async Task<IEnumerable<Propietario>> obtenerPropietariosActivosAsync()
        {
            return await _context.Propietario.Where(p => p.estado == EstadoPropietario.Activo).ToListAsync();
        }
    }
}
