using AerolineasWEB.BL;
using AerolineasWEB.Model;
using Microsoft.AspNetCore.Mvc;

namespace AerolineasWEB.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioAvionesController : ControllerBase
    {
        private readonly IAdministradorAvion _admin;

        public ServicioAvionesController(IAdministradorAvion admin)
        {
            _admin = admin;
        }

        [HttpGet("ObtenerAvionesActivos")]
        public async Task<ActionResult<IEnumerable<Avion>>> ObtenerListaAviones()
        {
            var lista = await _admin.ObtenerListaAvionesActivosAsync();
            return Ok(lista);
        }

        [HttpGet("ObtenerAvionPorId")]
        public async Task<ActionResult<Avion>> obtenerAvionPorId(int id)
        {
            var avion = await _admin.ObtenerAvionAsync(id);
            if (avion == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "No encontrado",
                    Detail = "El avión no existe.",
                    Status = 404
                });
            }
            return Ok(avion);
        }

        [HttpGet("ObtenerAvionesNombreAerolinea")]
        public async Task<ActionResult<IEnumerable<Avion>>> obtenerPorAerolinea(string nombre_aerolinea)
        {
            var lista = await _admin.ObtenerPorNombreAerolineaAsync(nombre_aerolinea);
            return Ok(lista);
        }

        [HttpGet("ObtenerAvionesNombrePropietario")]
        public async Task<ActionResult<IEnumerable<Avion>>> obtenerPorPropietario(string nombre_propietario)
        {
            var lista = await _admin.ObtenerPorNombrePropietarioAsync(nombre_propietario);
            return Ok(lista);
        }

        [HttpPost("AgregarAvion")]
        public async Task<IActionResult> AgregarAvion([FromBody] Avion avion)
        {
            await _admin.RegistrarAvionAsync(avion);
            return Ok();
        }

        [HttpPut("EditarAvion")]
        public async Task<IActionResult> EditarAvion([FromBody] Avion avion)
        {
            await _admin.EditarAvionAsync(avion);
            return Ok();
        }


        [HttpPut("EliminarAvion")]
        public async Task<IActionResult> EliminarAvion(int id)
        {
            await _admin.DesactivarAvionAsync(id);
            return Ok();
        }
    }
}
