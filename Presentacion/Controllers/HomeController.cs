using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentacion.Interfaces;
using Presentacion.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataSource dataSource;
        private readonly ILogger<HomeController> logger;

        public HomeController(IDataSource dataSource, ILogger<HomeController> logger)
        {
            this.dataSource = dataSource;
            this.logger = logger;
        }
        //public int tamPgn = 2;
        public async Task<ActionResult> Index(long? tipoID, int pageNumber = 1)
        {
            try
            {
                var productos = await dataSource.ListarProductos(pageNumber, 2, tipoID);
                var tipos = await dataSource.ListarTipos();
                var tiposDict = tipos.ToDictionary(t => t.TipoId, nom => nom.Nombre);
                foreach (var prod in productos)
                {
                    prod.TipoNombre = tiposDict[prod.TipoId];
                }  
                ViewBag.Tipos = tipos;
                ViewBag.TipoId = tipoID;
                return View(productos);
            }
            catch (System.Exception exp)
            {
                logger.LogError(exp, exp.Message);
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> CrearProducto()
        {
            try
            {
                var listaTipos = await dataSource.ListarTipos();
                ViewBag.Tipos = listaTipos;
                //throw new Exception("Error de prueba"); //para probar la pgn de error
                return View(new Product());  
            }
            catch (System.Exception exp)
            {
                logger.LogError(exp, exp.Message);
                return View("Error");
            }           
        }

        [HttpPost]
        public async Task<ActionResult> CrearProducto(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await dataSource.GuardarProducto(product);
                    return RedirectToAction("Index");
                }
                var listaTipos = await dataSource.ListarTipos();
                ViewBag.Tipos = listaTipos;
                return View(product);
            }
            catch (System.Exception exp )
            {
                logger.LogError(exp, exp.Message);
                return View("Error");
            }
        }
    }
}
