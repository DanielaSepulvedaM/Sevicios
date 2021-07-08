using Presentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.Interfaces
{
    //interfaz para manejar los 
    public interface IDataSource
    {
        Task<PaginatedList<Product>> ListarProductos(int pgn, int tamPgn, long? tipoID);
        Task<List<Tipo>> ListarTipos();
        Task GuardarProducto(Product product);
    }
}
