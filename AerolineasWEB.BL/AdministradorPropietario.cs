/*----------------------------------------------------------------------------------------------------------------
Reglas de Propietario
    -Identificación única al guardar y editar un propietario
    (PENDIENTE)Regla al desactivar (eliminar): no se puede desactivar un propietario si tiene aviones activos.
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

        public AdministradorPropietario(IPropietarioRepository propietarioRepository)
        {
            _propietarioRepository = propietarioRepository;
        }

        public Task DesactivarPropietarioAsync(int id)
        {
            throw new NotImplementedException();
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
            if (existente != null)
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
        }

        public Task<IEnumerable<Propietario>> ObtenerListaPropietariosActivosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Propietario> ObtenerPorIdentificacionAsync(string identificacion)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Propietario>> ObtenerPorNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task<Propietario> ObtenerPropietarioAsync(int id)
        {
            throw new NotImplementedException();
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
