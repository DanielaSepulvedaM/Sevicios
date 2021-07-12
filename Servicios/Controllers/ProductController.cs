using Datos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Servicios.Models;

namespace Servicios.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {      
        private TiendaDbContext context;
        public ProductController(TiendaDbContext ctx)
        {
            context = ctx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pgn"></param>
        /// <param name="tipoID"></param>
        /// <returns></returns>
        /// <remarks>
        /// api/product?pgn=1&tipoID=100
        /// </remarks>
        [HttpGet]
        public IActionResult GetProducts(int pgn, int tamPgn, long? tipoID)
        {
            var productosPaginados = context.Product.Where(n => tipoID.HasValue ? n.TipoId == tipoID.Value : true)
                    .Skip((pgn - 1) * tamPgn).Take(tamPgn).ToList();
            var cantidadProductosFiltrados = context.Product.Where(n => tipoID.HasValue ? n.TipoId == tipoID.Value : true).Count();

            var objeto = new
            {
                PaginaActual = pgn,
                TotalProductos = cantidadProductosFiltrados,
                Productos = productosPaginados
            };
            return Ok(objeto);
        }

        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Product.Add(product);
                context.SaveChanges();
                return Ok();
            }
            else
                return BadRequest(product);
        }
    }
}
