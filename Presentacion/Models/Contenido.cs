using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class Contenido
    {
        public int paginaActual { get; set; }
        public int totalProductos { get; set; }
        public List<Product> productos { get; set; }
    }
}
