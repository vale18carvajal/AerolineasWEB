using AerolineasWEB.BL;
using AerolineasWEB.Model;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AerolineasWEB.SI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioAerolineasController : ControllerBase
    {
        private readonly IAdministradorAerolinea _admin;
        public ServicioAerolineasController(IAdministradorAerolinea admin)
        {
            _admin = admin;
        }

        [HttpGet("ObtenerAerolineasActivas")]
        public async Task<ActionResult<IEnumerable<Aerolinea>>> ObtenerListaAerolineas()
        {
            var lista = await _admin.ObtenerListaAerolineasActivasAsync();
            return Ok(lista);
        }
        [HttpGet("ObtenerAerolineaPorId")]
        public async Task<ActionResult<Aerolinea>> obtenerAerolineaPorId(int id)
        {
            var aerolinea = await _admin.ObtenerAerolineaAsync(id);
            if (aerolinea == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "No encontrado",
                    Detail = "La aerolínea no existe.",
                    Status = 404
                });
            }
            return Ok(aerolinea);
        }
        [HttpGet("ObtenerAerolineaPorIata")]
        public async Task<ActionResult<IEnumerable<Aerolinea>>> obtenerAerolineaPorIata(string codigo_iata)
        {
            var lista = await _admin.ObtenerPorIataAsync(codigo_iata);
            return Ok(lista);
        }
        [HttpGet("ObtenerAerolineaPorIataExacto")]
        public async Task<ActionResult<Aerolinea>> obtenerAerolineaPorIataExacto(string codigo_iata)
        {
            var aerolinea = await _admin.ObtenerPorIataExactoAsync(codigo_iata);
            if (aerolinea == null)
            {
                return NotFound(new ProblemDetails
                {
                    Title = "No encontrado",
                    Detail = "La aerolínea no existe.",
                    Status = 404
                });
            }
            return Ok(aerolinea);
        }

        [HttpGet("ObtenerAerolineaPorNombre")]
        public async Task<ActionResult<IEnumerable<Aerolinea>>> obtenerAerolineaPorNombre(string nombre)
        {
            var lista = await _admin.ObtenerPorNombreAsync(nombre);
            return Ok(lista);
        }

        [HttpGet("ObtenerAerolineaPorTelefono")]
        public async Task<ActionResult<IEnumerable<Aerolinea>>> obtenerAerolineaPorTelefono(string telefono)
        {
            var lista = await _admin.ObtenerPorTelefonoAsync(telefono);
            return Ok(lista);
        }

        [HttpPost("AgregarAerolinea")]
        public async Task<IActionResult> AgregarAerolinea([FromBody] Aerolinea aerolinea)
        {
            //await _admin.RegistrarAerolineaAsync(aerolinea);
            //return Ok();
            try
            {
                await _admin.RegistrarAerolineaAsync(aerolinea);
                return Ok();
            }
            catch (ReglaNegocioException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = ex.Codigo,
                    Detail = ex.Message,
                    Status = 400
                });
            }
        }

        [HttpPut("EditarAerolinea")]
        public async Task<IActionResult> EditarAerolinea([FromBody] Aerolinea aerolinea)
        {
            //await _admin.EditarAerolineaAsync(aerolinea);
            //return Ok();
            try
            {
                await _admin.EditarAerolineaAsync(aerolinea);
                return Ok();
            }
            catch (ReglaNegocioException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = ex.Codigo,
                    Detail = ex.Message,
                    Status = 400
                });
            }
        }

        [HttpPut("EliminarAerolinea")]
        public async Task<IActionResult> EliminarAerolinea(int id)
        {
            //await _admin.DesactivarAerolineaAsync(id);
            //return Ok();

            try
            {
                await _admin.DesactivarAerolineaAsync(id);
                return Ok();
            }
            catch (ReglaNegocioException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = ex.Codigo,
                    Detail = ex.Message,
                    Status = 400
                });
            }
        }
    }
}
