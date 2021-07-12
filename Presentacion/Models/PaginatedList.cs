using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentacion.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PaginaActual { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPorPng { get; set; }
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PaginaActual = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }
        public bool PreviusPage
        {
            get
            {
                return (PaginaActual > 1);
            }
        }

        public bool NextPage
        {
            get
            {
                return (PaginaActual < TotalPages);
            }
        }
    }
}
