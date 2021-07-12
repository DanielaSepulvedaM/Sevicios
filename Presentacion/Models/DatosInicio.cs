using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presentacion.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class DatosInicio : IDataSource
    {
        public string Tipo { get; set; }
        public List<Product> Products = new List<Product> {
            new Product {ProductId = 1, Nombre = "blusa",Descripcion="blusa de prueba",Precio="25000",TipoId=2 },
            new Product {ProductId = 2, Nombre = "pantalon",Descripcion="pantalon de prueba",Precio="25000",TipoId=3 },
            new Product {ProductId = 3, Nombre = "jeans", Descripcion="jeans de prueba", Precio= "30000", TipoId = 1},
            new Product {ProductId = 4, Nombre = "falda", Descripcion="falda de prueba", Precio= "35000", TipoId = 2},
            new Product {ProductId = 5, Nombre = "pantaloneta", Descripcion="pantaloneta de prueba", Precio= "3000", TipoId = 1},
            new Product {ProductId = 6, Nombre = "pantalon",Descripcion="pantalon de prueba",Precio="25000",TipoId=3 },
            new Product {ProductId = 7, Nombre = "jeans", Descripcion="jeans de prueba", Precio= "30000", TipoId = 1},
            new Product {ProductId = 8, Nombre = "falda", Descripcion="falda de prueba", Precio= "35000", TipoId = 2},
            new Product {ProductId = 9, Nombre = "pantaloneta", Descripcion="pantaloneta de prueba", Precio= "3000", TipoId = 1}
        };
        public  List<Tipo> Tipos { get; } = new List<Tipo>
        {
            new Tipo{TipoId=1, Nombre = "TIPO A"},
            new Tipo{TipoId=2, Nombre = "TIPO B"},
            new Tipo{TipoId=3, Nombre = "TIPO C"},
        };
        public Task<PaginatedList<Product>> ListarProductos(int pgn, int tamPgn, long? tipoID)
        {
            var items = Products.Where(n => tipoID.HasValue ? n.TipoId == tipoID.Value : true)
                    .Skip((pgn - 1) * tamPgn).Take(tamPgn).ToList();
            return Task.FromResult(new PaginatedList<Product>(items, Products.Count(), pgn, tamPgn));
        }
        public Task<List<Tipo>> ListarTipos()
        {
            return Task.FromResult(Tipos.ToList());
        }
        public Task GuardarProducto(Product product)
        {
            Products.Add(product);
            return Task.CompletedTask;
        }
    }
}
