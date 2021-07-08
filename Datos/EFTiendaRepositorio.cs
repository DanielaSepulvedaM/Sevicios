using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos
{
    public class EFTiendaRepositorio: ITiendaRepositorio
    {
        private TiendaDbContext context;
        public EFTiendaRepositorio(TiendaDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Product;
       
    }
}
