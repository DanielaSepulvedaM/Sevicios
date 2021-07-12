using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Presentacion.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentacion.Interfaces
{
    public class ServicioDatasource : IDataSource
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ServicioDatasource> logger;
        public ServicioDatasource(IHttpClientFactory httpClientFactory, ILogger<ServicioDatasource> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<List<Tipo>> ListarTipos()
        {
            //CreateClient: sirve para crear un cliente http
            var client = httpClientFactory.CreateClient("ServicioProductos");
            var result = await client.GetAsync("api/tipo");
            if (result.IsSuccessStatusCode)
            {
                 var response1 = await result.Content.ReadAsStreamAsync();
                 var objeto1 = await JsonSerializer.DeserializeAsync<List<Tipo>>(response1, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true});
                 return objeto1;
            }
            else
                throw new System.Exception("No je pudo");
        }
        public async Task GuardarProducto(Product product)
        {
            var client = httpClientFactory.CreateClient("ServicioProductos");
            var respuesta = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var result = await client.PostAsync("api/product", respuesta);

            if (!result.IsSuccessStatusCode)
            {
                logger.LogError(result.StatusCode.ToString());
                throw new System.Exception("No je pudo");
            }
            
        }
        public async Task<PaginatedList<Product>> ListarProductos(int pgn, int tamPgn, long? tipoID)
        {
            var client = httpClientFactory.CreateClient("ServicioProductos");
            var parametros = new Dictionary<string, string>
            {
                { "pgn", pgn.ToString() },
                { "tamPgn", tamPgn.ToString() }
            };

            if  (tipoID.HasValue)
                parametros.Add("tipoID", tipoID.ToString());

            var url = QueryHelpers.AddQueryString("api/product", parametros); //api/product?pgn=#&tipoID=
            var result = await client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                using var response = await result.Content.ReadAsStreamAsync();
                var objeto = await JsonSerializer.DeserializeAsync<Contenido>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return new PaginatedList<Product>(objeto.productos, objeto.totalProductos, objeto.paginaActual, tamPgn);
            }
            else
                throw new System.Exception("No je pudo");
        }
    }
}