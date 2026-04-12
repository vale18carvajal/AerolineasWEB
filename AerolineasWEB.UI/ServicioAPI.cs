using AerolineasWEB.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AerolineasWEB.UI
{
    public class ServicioAPI
    {
        private readonly IHttpClientFactory _httpClientFactory; 
        
        public ServicioAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //AEROLINEAS
        //GET
        public async Task<List<Aerolinea>> ObtenerAerolineasActivasAsync()
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync("api/ServicioAerolineas/ObtenerAerolineasActivas");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Aerolinea>>(result) ?? [];
            return lista;
        }
        public async Task<Aerolinea?> ObtenerAerolineaPorIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioAerolineas/ObtenerAerolineaPorId?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var aerolinea = JsonConvert.DeserializeObject<Aerolinea>(result);
                return aerolinea;
            }
            return null;
        }
        public async Task<List<Aerolinea>> ObtenerAerolineaPorIATA(string codigo_iata)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioAerolineas/ObtenerAerolineaPorIata?codigo_iata={codigo_iata}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Aerolinea>>(result) ?? [];
            return lista;
        }
        public async Task<List<Aerolinea>> ObtenerAerolineasPorNombreAsync(string nombre)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioAerolineas/ObtenerAerolineaPorNombre?nombre={nombre}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Aerolinea>>(result) ?? [];
            return lista;
        }
        public async Task<List<Aerolinea>> ObtenerAerolineaPorTelefonoAsync(string telefono)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioAerolineas/ObtenerAerolineaPorTelefono?telefono={telefono}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Aerolinea>>(result) ?? [];
            return lista;
        }
        //INSERT
        public async Task AgregarAerolineaAsync(Aerolinea aerolinea)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var json = JsonConvert.SerializeObject(aerolinea);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/ServicioAerolineas/AgregarAerolinea", content);
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }
        //UPDATE
        public async Task EditarAerolineaAsync(Aerolinea aerolinea)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var json = JsonConvert.SerializeObject(aerolinea);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/ServicioAerolineas/EditarAerolinea", content);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }
        public async Task EliminarAerolineaAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.PutAsync($"api/ServicioAerolineas/EliminarAerolinea?id={id}", null);
            //response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }

        //AVIONES
        //GET
        public async Task<List<Avion>> ObtenerAvionesActivosAsync()
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync("api/ServicioAviones/ObtenerAvionesActivos");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Avion>>(result) ?? [];
            return lista;
        }
        public async Task<Avion?> ObtenerAvionPorIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioAviones/ObtenerAvionPorId?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var avion = JsonConvert.DeserializeObject<Avion>(result);
                return avion;
            }
            return null;
        }
        public async Task<List<Avion>> ObtenerAvionesPorNombreAerolineaAsync(string nombre_aerolinea)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioAviones/ObtenerAvionesNombreAerolinea?nombre_aerolinea={nombre_aerolinea}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Avion>>(result) ?? [];
            return lista;
        }
        public async Task<List<Avion>> ObtenerAvionesPorNombrePropietarioAsync(string nombre_propietario)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioAviones/ObtenerAvionesNombrePropietario?nombre_propietario={nombre_propietario}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Avion>>(result) ?? [];
            return lista;
        }
        //INSERT
        public async Task AgregarAvionAsync(Avion avion)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var json = JsonConvert.SerializeObject(avion);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/ServicioAviones/AgregarAvion", content);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }
        //UPDATE
        public async Task EditarAvionAsync(Avion avion)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var json = JsonConvert.SerializeObject(avion);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/ServicioAviones/EditarAvion", content);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }
        public async Task EliminarAvionAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.PutAsync($"api/ServicioAviones/EliminarAvion?id={id}", null);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }

        //PROPIETARIOS
        //GET
        public async Task<List<Propietario>> ObtenerPropietariosActivosAsync()
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync("api/ServicioPropietarios/ObtenerPropietariosActivos");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Propietario>>(result) ?? [];
            return lista;
        }
        public async Task<Propietario?> ObtenerPropietarioPorIdAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioPropietarios/ObtenerPropietarioPorId?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var propietario = JsonConvert.DeserializeObject<Propietario>(result);
                return propietario;
            }
            return null;
        }
        public async Task<Propietario?> ObtenerPropietarioPorIdentificacionExactaAsync(string identificacion)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioPropietarios/ObtenerPropietarioPorIdentificacion?identificacion={identificacion}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var propietario = JsonConvert.DeserializeObject<Propietario>(result);
                return propietario;
            }
            return null;
        }
        public async Task<List<Propietario>> ObtenerPropietarioPorIdentificacionAsync(string identificacion)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioPropietarios/ObtenerPropietarioPorIdentificacion?identificacion={identificacion}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Propietario>>(result) ?? [];
            return lista;
        }
        public async Task<List<Propietario>> ObtenerPropietariosPorNombreAsync(string nombre)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.GetAsync($"api/ServicioPropietarios/ObtenerPropietarioPorNombre?nombre={nombre}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var lista = JsonConvert.DeserializeObject<List<Propietario>>(result) ?? [];
            return lista;
        }
        //INSERT
        public async Task AgregarPropietarioAsync(Propietario propietario)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var json = JsonConvert.SerializeObject(propietario);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/ServicioPropietarios/AgregarPropietario", content);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }
        //UPDATE
        public async Task EditarPropietarioAsync(Propietario propietario)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var json = JsonConvert.SerializeObject(propietario);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/ServicioPropietarios/EditarPropietario", content);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }
        public async Task EliminarPropietariosAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("AerolineasWebAPI");
            var response = await client.PutAsync($"api/ServicioPropietarios/EliminarPropietario?id={id}", null);
            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
                throw new Exception(problem.Detail);
            }
        }
    }
}
