using Datos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : Controller
    {
        private TiendaDbContext context;

        public TipoController(TiendaDbContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IAsyncEnumerable<Tipo> GetTipos()
        {
            return context.Tipo;
        }
    }
}
