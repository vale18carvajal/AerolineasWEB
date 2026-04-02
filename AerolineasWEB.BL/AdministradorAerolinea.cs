
/*----------------------------------------------------------------------------------------------------------------
Reglas de Aerolínea
    -Código IATA único al guardar y editar una aerolínea
    -Fecha válida que no sea mayor a la fecha actual en crear y editar aerolínea
    (PENDIENTE)Regla al desactivar (eliminar): no se puede desactivar una aerolínea si tiene aviones activos.
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

        public AdministradorAerolinea (IAerolineaRepository aerolineaRepository)
        {
            _aerolineaRepository = aerolineaRepository;
        }
        public async Task DesactivarAerolineaAsync(int id)
        {

            await _aerolineaRepository.desactivarAsync(id);
        }

        public async Task EditarAerolineaAsync(Aerolinea aerolinea)
        {
            Aerolinea aerolineaEditar = await _aerolineaRepository.obtenerPorIdAsync(aerolinea.id_aerolinea);
            if (aerolineaEditar == null)
            {
                throw new ReglaNegocioException("Error", "No se encontró aerolínea a editar.");
            }

            if (aerolineaEditar.estado == EstadoAerolinea.Inactivo)
            {
                throw new ReglaNegocioException("Error", "No se puede editar aerolíneas inactivas.");
            }

            var existente = await _aerolineaRepository.obtenerPorIataAsync(aerolinea.codigo_iata);
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


            aerolinea.nombre = aerolinea.nombre.Trim();
            aerolinea.codigo_iata = aerolinea.codigo_iata.Trim().ToUpper();

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

        public async Task<Aerolinea> ObtenerPorIataAsync(string codigo_iata)
        {
            return await _aerolineaRepository.obtenerPorIataAsync(codigo_iata);
        }

        public async Task<IEnumerable<Aerolinea>> ObtenerPorNombreAsync(string nombre)
        {
            return await _aerolineaRepository.obtenerPorNombreAsync(nombre);
        }

        public async Task<Aerolinea> ObtenerPorTelefonoAsync(string telefono)
        {
            return await _aerolineaRepository.obtenerPorTelefonoAsync(telefono);
        }

        public async Task RegistrarAerolineaAsync(Aerolinea aerolinea)
        {
            var existente = await _aerolineaRepository.obtenerPorIataAsync(aerolinea.codigo_iata);
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

            aerolinea.nombre = aerolinea.nombre.Trim();
            aerolinea.codigo_iata = aerolinea.codigo_iata.Trim().ToUpper();
            aerolinea.estado = EstadoAerolinea.Activo;

            await _aerolineaRepository.crearAsync(aerolinea);
        }
    }
}
