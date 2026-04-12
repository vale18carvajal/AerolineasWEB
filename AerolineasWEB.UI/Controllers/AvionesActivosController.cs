using AerolineasWEB.Model;
using AerolineasWEB.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Drawing;

namespace AerolineasWEB.UI.Controllers
{
    public class AvionesActivosController(ServicioAPI servicioAPI) : Controller
    {
        // GET: AvionesActivosController
        public async Task<ActionResult> Index(string tipo_busqueda, string valor)
        {
            //var listaAviones = await servicioAPI.ObtenerAvionesActivosAsync();
            //return View(listaAviones);

            IEnumerable<Avion> listaAviones;

            if (!string.IsNullOrEmpty(valor))
            {
                switch (tipo_busqueda)
                {
                    case "nombre_aerolinea":
                        listaAviones = await servicioAPI.ObtenerAvionesPorNombreAerolineaAsync(valor);
                        ViewBag.Filtro = true;
                        ViewBag.BusquedaTipo = tipo_busqueda;
                        ViewBag.Valor = valor;
                        break;

                    case "nombre_propietario":
                        listaAviones = await servicioAPI.ObtenerAvionesPorNombrePropietarioAsync(valor);
                        ViewBag.Filtro = true;
                        ViewBag.BusquedaTipo = tipo_busqueda;
                        ViewBag.Valor = valor;
                        break;
                    
                    default:
                        listaAviones = await servicioAPI.ObtenerAvionesActivosAsync();
                        break;
                }
            }
            else
            {
                listaAviones = await servicioAPI.ObtenerAvionesActivosAsync();
            }

            return View(listaAviones);

        }

        // GET: AvionesActivosController/Create
        public async Task<ActionResult> Create()
        {
            try
            {
               

                var aerolineas = await servicioAPI.ObtenerAerolineasActivasAsync();
                var propietarios = await servicioAPI.ObtenerPropietariosActivosAsync();

                var modelo = new AvionFormViewModel
                {
                    Aerolineas = aerolineas,
                    Propietarios = propietarios
                };

                //return View(modelo);
                return View(modelo);
            } catch
            {
                return View();
            }
            
        }

        // POST: AvionesActivosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Avion avion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await MapearAViewModel(avion));
                }

                await servicioAPI.AgregarAvionAsync(avion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //ViewBag.Error = ex.Message;
                //return View(avion);
                ViewBag.Error = ex.Message;
                return View(await MapearAViewModel(avion));
            }
        }

        // GET: AvionesActivosController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                //var avion = await servicioAPI.ObtenerAvionPorIdAsync(id);
                //return View(avion);
                var avion = await servicioAPI.ObtenerAvionPorIdAsync(id);
                if (avion == null) { return RedirectToAction(nameof(Index));}
                return View(await MapearAViewModel(avion));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: AvionesActivosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Avion avion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(await MapearAViewModel(avion));
                }
                await servicioAPI.EditarAvionAsync(avion);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(await MapearAViewModel(avion));
            }
        }

        // POST: AvionesActivosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await servicioAPI.EliminarAvionAsync(id);
                TempData["Success"] = "El avión se ha eliminado correctamente";
                TempData["FromSuccess"] = true;
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                TempData["FromDelete"] = true;
            }
            return RedirectToAction(nameof(Index));
        }
        private async Task<AvionFormViewModel> MapearAViewModel(Avion avion)
        {
            var aerolineas = await servicioAPI.ObtenerAerolineasActivasAsync();
            var propietarios = await servicioAPI.ObtenerPropietariosActivosAsync();

            return new AvionFormViewModel
            {
                id_avion = avion.id_avion,
                matricula = avion.matricula,
                modelo = avion.modelo,
                fabricante = avion.fabricante,
                cantidad_pasajeros = avion.cantidad_pasajeros,
                anio = avion.anio,
                estado = avion.estado,
                id_aerolinea = avion.id_aerolinea,
                id_propietario = avion.id_propietario,

                Aerolineas = aerolineas,
                Propietarios = propietarios
            };
        }
    }
}
