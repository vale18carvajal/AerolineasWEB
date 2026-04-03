/*----------------------------------------------------------------------------------------------------------------
Reglas de Propietario
    -Identificación única al guardar y editar un propietario
    -Regla al desactivar (eliminar): no se puede desactivar un propietario si tiene aviones activos.
    -Normalizar datos de nombre: quitar espacios en blanco con Trim.
    -No se pueden editar propietarios inactivos.
    -No se edita estado en la función de editar
    -Al agregar propietario, siempre es activo.
----------------------------------------------------------------------------------------------------------------*/

using AerolineasWEB.Model;


namespace AerolineasWEB.BL
{
    public class AdministradorPropietario : IAdministradorPropietario
    {
        private readonly IPropietarioRepository _propietarioRepository;
        private readonly IAvionRepository _avionRepository;

        public AdministradorPropietario(IPropietarioRepository propietarioRepository, IAvionRepository avionRepository)
        {
            _propietarioRepository = propietarioRepository;
            _avionRepository = avionRepository;
        }

        public async Task DesactivarPropietarioAsync(int id)
        {
            var tieneAviones = await _avionRepository.ExistenAvionesActivosPorPropietario(id);
            if (tieneAviones)
            {
                throw new ReglaNegocioException("Error", "No se puede eliminar propietario porque tiene aviones activos.");
            }
            await _propietarioRepository.desactivarAsync(id);
        }

        public async Task EditarPropietarioAsync(Propietario propietario)
        {
            Propietario propietarioEditar = await _propietarioRepository.obtenerPorIdAsync(propietario.id_propietario);
            if (propietarioEditar == null)
            {
                throw new ReglaNegocioException("Error", "No se encontró propietario a editar.");
            }

            if (propietarioEditar.estado == EstadoPropietario.Inactivo)
            {
                throw new ReglaNegocioException("Error", "No se pueden editar propietarios inactivos.");
            }

            var existente = await _propietarioRepository.obtenerPorIdentificacionPropietarioAsync(propietario.identificacion);
            if (existente != null && existente.id_propietario != propietario.id_propietario)
            {
                throw new ReglaNegocioException("Error", "Ya existe un propietario con la identificación ingresada.");
            }

            propietario.nombre = propietario.nombre.Trim();

            propietarioEditar.identificacion = propietario.identificacion;
            propietarioEditar.nombre = propietario.nombre;
            propietarioEditar.tipo = propietario.tipo;
            propietarioEditar.pais = propietario.pais;
            propietarioEditar.telefono = propietario.telefono;
            propietarioEditar.correo = propietario.correo;

            await _propietarioRepository.editarAsync(propietarioEditar);
        }

        public async Task<IEnumerable<Propietario>> ObtenerListaPropietariosActivosAsync()
        {
            return await _propietarioRepository.obtenerPropietariosActivosAsync();
        }

        public async Task<Propietario> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _propietarioRepository.obtenerPorIdentificacionPropietarioAsync(identificacion);
        }

        public async Task<IEnumerable<Propietario>> ObtenerPorNombreAsync(string nombre)
        {
            return await _propietarioRepository.obtenerPorNombreAsync(nombre);
        }

        public async Task<Propietario> ObtenerPropietarioAsync(int id)
        {
            return await _propietarioRepository.obtenerPorIdAsync(id);
        }

        public async Task RegistrarPropietarioAsync(Propietario propietario)
        {
            var existente = await _propietarioRepository.obtenerPorIdentificacionPropietarioAsync(propietario.identificacion);
            if (existente != null)
            {
                throw new ReglaNegocioException("Error", "Ya existe un propietario con la identificación ingresada.");
            }

            propietario.nombre = propietario.nombre.Trim();
            propietario.estado = EstadoPropietario.Activo;

            await _propietarioRepository.crearAsync(propietario);
        }
    }
}
