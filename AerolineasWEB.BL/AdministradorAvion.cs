/*----------------------------------------------------------------------------------------------------------------
Reglas de Avión
    [PENDIENTE) Matrícula única al editar y agregar
    -Normalizar datos con Trim y Upper case en modelo, matrícula al agregar y editar
    -Año válido que no sea mayor a la fecha actual en crear y editar avión.
    -No se pueden editar aviones inactivos.
    -No se edita estado en la función de editar
----------------------------------------------------------------------------------------------------------------*/

using AerolineasWEB.Model;

namespace AerolineasWEB.BL
{
    public class AdministradorAvion : IAdministradorAvion
    {
        private readonly IAvionRepository _avionRepository;

        public AdministradorAvion(IAvionRepository avionRepository)
        {
            _avionRepository = avionRepository;
        }

        public async Task DesactivarAvionAsync(int id)
        {
            await _avionRepository.desactivarAsync(id);
        }

        public async Task EditarAvionAsync(Avion avion)
        {
            Avion avionEditar = await _avionRepository.obtenerPorIdAsync(avion.id_avion);
            if (avionEditar == null)
            {
                throw new ReglaNegocioException("Error", "No se encontró avión a editar.");
            }

            if (avionEditar.estado == EstadoAvion.Inactivo)
            {
                throw new ReglaNegocioException("Error", "No se pueden editar aviones inactivos.");
            }

            /*var existente = await _avionRepository.obtenerPorMatricula(avion.matricula);
            if (existente != null && existente.id_avion != aerolineaEditar.id_avion) //Se valida que no sea otro avión al que estamos editando con el mismo IATA
            {
                throw new ReglaNegocioException("Error", "Ya existe una avión con la matrícula ingresada.");
            }*/

            if (!int.TryParse(avion.anio, out int anio))
            {
                throw new ReglaNegocioException("Error", "El año debe ser numérico.");
            }

            if (anio > DateTime.Now.Year)
            {
                throw new ReglaNegocioException("Error", "El año no puede ser mayor al actual.");
            }

            avion.matricula = avion.matricula.Trim().ToUpper();
            avion.modelo = avion.modelo.Trim().ToUpper();
            //avion.anio = avion.anio.ToString();

            avionEditar.matricula = avion.matricula;
            avionEditar.modelo = avion.modelo;
            avionEditar.fabricante = avion.fabricante;
            avionEditar.cantidad_pasajeros = avion.cantidad_pasajeros;
            avionEditar.anio = avion.anio;
            avionEditar.id_aerolinea = avion.id_aerolinea;
            avionEditar.id_propietario = avion.id_propietario;

            await _avionRepository.editarAsync(avionEditar);

        }

        public async Task<Avion> ObtenerAvionAsync(int id)
        {
            return await _avionRepository.obtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Avion>> ObtenerListaAvionesActivosAsync()
        {
            return await _avionRepository.obtenerAvionesActivosAsync();
        }

        public async Task<IEnumerable<Avion>> ObtenerPorNombreAerolineaAsync(string nombreAerolinea)
        {
            return await _avionRepository.obtenerPorNombreAerolineaAsync(nombreAerolinea);
        }

        public async Task<IEnumerable<Avion>> ObtenerPorNombrePropietarioAsync(string nombrePropietario)
        {
            return await _avionRepository.obtenerPorNombrePropietarioAsync(nombrePropietario);
        }

        public async Task RegistrarAvionAsync(Avion avion)
        {
            /*var existente = await _avionRepository.obtenerPorMatricula(avion.matricula);
            if (existente != null)
            {
                throw new ReglaNegocioException("Error", "Ya existe una avión con la matrícula ingresada.");
            }*/

            if (!int.TryParse(avion.anio, out int anio))
            {
                throw new ReglaNegocioException("Error", "El año debe ser numérico.");
            }

            if (anio > DateTime.Now.Year)
            {
                throw new ReglaNegocioException("Error", "El año no puede ser mayor al actual.");
            }

            avion.matricula = avion.matricula.Trim().ToUpper();
            avion.modelo = avion.modelo.Trim().ToUpper();

            await _avionRepository.crearAsync(avion);

        }
    }
}
