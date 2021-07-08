using Microsoft.AspNetCore.Mvc;
using Presentacion.Interfaces;
using Presentacion.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataSource dataSource;

        public HomeController(IDataSource  dataSource){
            this.dataSource = dataSource;
        }
        //public int tamPgn = 2;
        public async Task<ActionResult> Index(long? tipoID, int pageNumber = 1) {
            var productos = await dataSource.ListarProductos(pageNumber,2,tipoID);
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

        [HttpGet]
        public async Task<ActionResult> CrearProducto() {

            var listaTipos = await dataSource.ListarTipos();
            ViewBag.Tipos = listaTipos;
            return View(new Product());
        }

        [HttpPost]
        public async Task<ActionResult> CrearProducto(Product product)
        {
            if (ModelState.IsValid)
            {
                await dataSource.GuardarProducto(product);
                return RedirectToAction("Index");
               
            }
            var listaTipos = await dataSource.ListarTipos();
            ViewBag.Tipos =  listaTipos;
            return View(product);

        }

    }
}
