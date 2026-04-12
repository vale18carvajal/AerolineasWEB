
/*----------------------------------------------------------------------------------------------------------------
Reglas de Aerolínea
    -Código IATA único al guardar y editar una aerolínea
    -Fecha válida que no sea mayor a la fecha actual en crear y editar aerolínea
    -Regla al desactivar (eliminar): no se puede desactivar una aerolínea si tiene aviones activos.
    -Normalizar datos de nombre y codigo IATA: quitar espacios en blanco con Trim y IATA que sea mayúsuculas.
    -No se pueden editar aerolíneas inactivas.
    -IATA debe tener mínimo 2 caracteres al agregar y editar aerolínea
    -No se edita estado en la función de editar
    -Al agregar aerolínea, siempre es activa.
----------------------------------------------------------------------------------------------------------------*/
using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public class AdministradorAerolinea : IAdministradorAerolinea
    {
        private readonly IAerolineaRepository _aerolineaRepository;
        private readonly IAvionRepository _avionRepository;

        public AdministradorAerolinea (IAerolineaRepository aerolineaRepository, IAvionRepository avionRepository)
        {
            _aerolineaRepository = aerolineaRepository;
            _avionRepository = avionRepository;
        }
        public async Task DesactivarAerolineaAsync(int id)
        {
            var tieneAviones = await _avionRepository.ExistenAvionesActivosPorAerolinea(id);
            if (tieneAviones)
            {
                throw new ReglaNegocioException("Error", "No se puede eliminar la aerolínea porque tiene aviones activos.");
            }
            await _aerolineaRepository.desactivarAsync(id);
        }

        public async Task EditarAerolineaAsync(Aerolinea aerolinea)
        {
            aerolinea.nombre = aerolinea.nombre.Trim();
            aerolinea.codigo_iata = aerolinea.codigo_iata.Trim().ToUpper();

            Aerolinea aerolineaEditar = await _aerolineaRepository.obtenerPorIdAsync(aerolinea.id_aerolinea);
            if (aerolineaEditar == null)
            {
                throw new ReglaNegocioException("Error", "No se encontró aerolínea a editar.");
            }

            if (aerolineaEditar.estado == EstadoAerolinea.Inactivo)
            {
                throw new ReglaNegocioException("Error", "No se puede editar aerolíneas inactivas.");
            }

            var existente = await _aerolineaRepository.obtenerPorIataExactoAsync(aerolinea.codigo_iata);
            if (existente != null && existente.id_aerolinea != aerolineaEditar.id_aerolinea) //Se valida que no sea otra aerolínea a la que estamos editando con el mismo IATA
            {
                throw new ReglaNegocioException("Error", "Ya existe una aerolínea con ese código IATA.");
            }

            if (aerolinea.fecha_fundacion > DateTime.Now)
            {
                throw new ReglaNegocioException("Error", "La fecha de fundación no puede ser futura.");
            }

            if (aerolinea.codigo_iata.Length < 2)
            {
                throw new ReglaNegocioException("Error", "El código IATA debe tener al menos 2 caracteres.");
            }

            aerolineaEditar.nombre = aerolinea.nombre;
            aerolineaEditar.codigo_iata = aerolinea.codigo_iata;
            aerolineaEditar.pais = aerolinea.pais;
            aerolineaEditar.fecha_fundacion = aerolinea.fecha_fundacion;
            aerolineaEditar.telefono = aerolinea.telefono;
            await _aerolineaRepository.editarAsync(aerolineaEditar);
        }

        public async Task<Aerolinea> ObtenerAerolineaAsync(int id)
        {
           return await _aerolineaRepository.obtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Aerolinea>> ObtenerListaAerolineasActivasAsync()
        {
            return await _aerolineaRepository.obtenerAerolineasActivasAsync();
        }

        public async Task<IEnumerable<Aerolinea>> ObtenerPorIataAsync(string codigo_iata)
        {
            return await _aerolineaRepository.obtenerPorIataAsync(codigo_iata);
        }

        public async Task<Aerolinea> ObtenerPorIataExactoAsync(string codigo_iata)
        {
            return await _aerolineaRepository.obtenerPorIataExactoAsync(codigo_iata);
        }

        public async Task<IEnumerable<Aerolinea>> ObtenerPorNombreAsync(string nombre)
        {
            return await _aerolineaRepository.obtenerPorNombreAsync(nombre);
        }

        public async Task<IEnumerable<Aerolinea>> ObtenerPorTelefonoAsync(string telefono)
        {
            return await _aerolineaRepository.obtenerPorTelefonoAsync(telefono);
        }

        public async Task RegistrarAerolineaAsync(Aerolinea aerolinea)
        {
            aerolinea.nombre = aerolinea.nombre.Trim();
            aerolinea.codigo_iata = aerolinea.codigo_iata.Trim().ToUpper();

            var existente = await _aerolineaRepository.obtenerPorIataExactoAsync(aerolinea.codigo_iata);
            if (existente != null)
            {
                throw new ReglaNegocioException("Error", "Ya existe una aerolínea con ese código IATA.");
            }
            if (aerolinea.fecha_fundacion > DateTime.Now)
            {
                throw new ReglaNegocioException("Error", "La fecha de fundación no puede ser futura");
            }

            if (aerolinea.codigo_iata.Length < 2)
            {
                throw new ReglaNegocioException("Error", "El código IATA debe tener al menos 2 caracteres.");
            }

            aerolinea.estado = EstadoAerolinea.Activo;

            await _aerolineaRepository.crearAsync(aerolinea);
        }
    }
}
