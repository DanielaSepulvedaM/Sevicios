using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Introduzca el Id del producto")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre del producto")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Introduzca la descripcion del producto")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Introduzca el precio del producto")]
        public string Precio { get; set; }

        [Required(ErrorMessage = "Especificar un tipo de producto")]
        public long TipoId { get; set; }
        public string TipoNombre { get; set; }

    }
}
