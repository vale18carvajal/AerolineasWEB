using AerolineasWEB.BL;
using AerolineasWEB.Model;
using Microsoft.AspNetCore.Mvc;

namespace AerolineasWEB.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioPropietariosController : Controller
    {
        private readonly IAdministradorPropietario _admin;
        public ServicioPropietariosController(IAdministradorPropietario admin)
        {
            _admin = admin;
        }

        [HttpGet("ObtenerPropietariosActivos")]
        public async Task<ActionResult<IEnumerable<Propietario>>> ObtenerListaPropietarios()
        {
            var lista = await _admin.ObtenerListaPropietariosActivosAsync();
            return Ok(lista);
        }

        [HttpGet("ObtenerPropietarioPorId")]
        public async Task<ActionResult<Propietario>> obtenerPropietarioPorId(int id)
        {
            var propietario = await _admin.ObtenerPropietarioAsync(id);
            if (propietario == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "No encontrado",
                    Detail = "El propietario no existe.",
                    Status = 404
                });
            }
            return Ok(propietario);
        }

        [HttpGet("ObtenerPropietarioPorIdentificacion")]
        public async Task<ActionResult<Propietario>> obtenerPropietarioPorIdentificacion(string identificacion)
        {
            var propietario = await _admin.ObtenerPorIdentificacionAsync(identificacion);
            if (propietario == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "No encontrado",
                    Detail = "No existe propietario con la identificación especificada.",
                    Status = 404
                });
            }
            return Ok(propietario);
        }

        [HttpGet("ObtenerPropietarioPorNombre")]
        public async Task<ActionResult<IEnumerable<Propietario>>> obtenerPropietarioPorNombre(string nombre)
        {
            var lista = await _admin.ObtenerPorNombreAsync(nombre);
            return Ok(lista);
        }

        [HttpPost("AgregarPropietario")]
        public async Task<IActionResult> AgregarPropietario([FromBody] Propietario propietario)
        {
            await _admin.RegistrarPropietarioAsync(propietario);
            return Ok();
        }

        [HttpPut("EditarPropietario")]
        public async Task<IActionResult> EditarPropietario([FromBody] Propietario propietario)
        {
            await _admin.EditarPropietarioAsync(propietario);
            return Ok();
        }

        [HttpPut("EliminarPropietario")]
        public async Task<IActionResult> EliminarPropietario(int id)
        {
            await _admin.DesactivarPropietarioAsync(id);
            return Ok();
        }

    }
}
