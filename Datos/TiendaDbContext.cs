using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class TiendaDbContext: DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
        public DbSet<Tipo> Tipo { get; set; }
    }
}
