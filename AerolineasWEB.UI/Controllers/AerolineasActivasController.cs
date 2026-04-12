using AerolineasWEB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Threading.Tasks;

namespace AerolineasWEB.UI.Controllers
{
    public class AerolineasActivasController(ServicioAPI servicioAPI) : Controller
    {
        // GET: AerolineasActivasController
        /*public async Task<ActionResult> Index()
        {
            var listaAerolineas = await servicioAPI.ObtenerAerolineasActivasAsync();
            return View(listaAerolineas);
        }*/

        public async Task<ActionResult> Index(string tipo_busqueda, string valor)
        {
            IEnumerable<Aerolinea> listaAerolineas;
            Aerolinea? aerolinea;

            if (!string.IsNullOrEmpty(valor))
            {
                switch (tipo_busqueda)
                {
                    case "nombre":
                        listaAerolineas = await servicioAPI.ObtenerAerolineasPorNombreAsync(valor);
                        ViewBag.Filtro = true;
                        ViewBag.BusquedaTipo = tipo_busqueda;
                        ViewBag.Valor = valor;
                        break;

                    case "iata":
                        listaAerolineas = await servicioAPI.ObtenerAerolineaPorIATA(valor);
                        ViewBag.Filtro = true;
                        ViewBag.BusquedaTipo = tipo_busqueda;
                        ViewBag.Valor = valor;
                        break;
                    case "telefono":
                        listaAerolineas = await servicioAPI.ObtenerAerolineaPorTelefonoAsync(valor);
                        ViewBag.Filtro = true;
                        ViewBag.BusquedaTipo = tipo_busqueda;
                        ViewBag.Valor = valor;
                        break;

                    default:
                        listaAerolineas = await servicioAPI.ObtenerAerolineasActivasAsync();
                        break;
                }
            }
            else
            {
                listaAerolineas = await servicioAPI.ObtenerAerolineasActivasAsync();
            }

            return View(listaAerolineas);
        }


        // GET: AerolineasActivasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AerolineasActivasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Aerolinea aerolinea)
        {
            try
            {
                await servicioAPI.AgregarAerolineaAsync(aerolinea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(aerolinea);
            }
        }

        // GET: AerolineasActivasController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var aerolinea = await servicioAPI.ObtenerAerolineaPorIdAsync(id);
                return View(aerolinea);
            } catch
            {
                return RedirectToAction(nameof(Index));
            }
            
        }

        // POST: AerolineasActivasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Aerolinea aerolinea)
        {
            try
            {
                await servicioAPI.EditarAerolineaAsync(aerolinea);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(aerolinea);
            }
        }

        // POST: AerolineasActivasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await servicioAPI.EliminarAerolineaAsync(id);
                //return RedirectToAction(nameof(Index));
                TempData["Success"] = "La aerolínea se ha eliminado";
                TempData["FromSuccess"] = true;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                TempData["FromDelete"] = true;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
