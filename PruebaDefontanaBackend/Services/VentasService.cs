using PruebaDefontanaBackend.DatabaseFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

using PruebaDefontanaBackend.DatabaseFiles.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace PruebaDefontanaBackend.Services
{
    internal class VentasService
    {
        PruebaContext Context;

        internal VentasService(PruebaContext context)=>Context = context;

        public List<Venta> ObtenerVentas(int dias) => Context.Venta.Where(i=> i.Fecha.Date > DateTime.Now.AddDays((dias *-1))  && i.Fecha < DateTime.Now)
    .Include(i => i.Local)
    .Include(i => i.VentaDetalles)
    .ThenInclude(i => i.Producto)
    .ThenInclude(i=> i.Marca)
    .ToList();




    }
}
