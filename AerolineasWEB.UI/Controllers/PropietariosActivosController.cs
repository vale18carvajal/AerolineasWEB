using AerolineasWEB.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;

namespace AerolineasWEB.UI.Controllers
{
    public class PropietariosActivosController(ServicioAPI servicioAPI) : Controller
    {
        // GET: PropietariosController
        public async Task<ActionResult> Index(string tipo_busqueda, string valor)
        {
            //var listadoPropietarios = await servicioAPI.ObtenerPropietariosActivosAsync();
            //return View(listadoPropietarios);
            IEnumerable<Propietario> listaPropietarios;
            Propietario? propietario;

            if (!string.IsNullOrEmpty(valor))
            {
                switch (tipo_busqueda)
                {
                    case "nombre_propietario":
                        listaPropietarios = await servicioAPI.ObtenerPropietariosPorNombreAsync(valor);
                        ViewBag.Filtro = true;
                        ViewBag.BusquedaTipo = tipo_busqueda;
                        ViewBag.Valor = valor;
                        break;

                    case "identificacion":
                        listaPropietarios = await servicioAPI.ObtenerPropietarioPorIdentificacionAsync(valor);
                        ViewBag.Filtro = true;
                        ViewBag.BusquedaTipo = tipo_busqueda;
                        ViewBag.Valor = valor;
                        break;
                  
                    default:
                        listaPropietarios = await servicioAPI.ObtenerPropietariosActivosAsync();
                        break;
                }
            }
            else
            {
                listaPropietarios = await servicioAPI.ObtenerPropietariosActivosAsync();
            }

            return View(listaPropietarios);

        }

        // GET: PropietariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropietariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Propietario propietario)
        {
            try
            {
                await servicioAPI.AgregarPropietarioAsync(propietario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(propietario);
            }
        }

        // GET: PropietariosController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var propietario = await servicioAPI.ObtenerPropietarioPorIdAsync(id);
                if (propietario == null) { return RedirectToAction(nameof(Index)); }
                return View(propietario);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PropietariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Propietario propietario)
        {
            try
            {
                await servicioAPI.EditarPropietarioAsync(propietario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(propietario);
            }
        }

        // POST: PropietariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {          
            try
            {
                await servicioAPI.EliminarPropietariosAsync(id);
                //return RedirectToAction(nameof(Index));
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
    }
}
