using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos
{
    public interface ITiendaRepositorio
    {
        IQueryable<Product> Products { get; }
    }
}
