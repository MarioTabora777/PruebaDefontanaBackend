using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using PruebaDefontanaBackend.DatabaseFiles.Tables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefontanaBackend.DatabaseFiles
{
    public class PruebaContext:DbContext
    { 
       
        public PruebaContext(DbContextOptions options) : base(options){}
       
        public DbSet<Local> Local { get; set; }

        public DbSet<Marca> Marca { get; set; }

        public DbSet<Producto> Producto { get; set; }

        public DbSet<Venta> Venta { get; set; }
        public DbSet<VentaDetalle> VentaDetalle { get; set; }





    }
}
