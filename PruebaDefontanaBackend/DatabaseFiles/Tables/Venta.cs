using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefontanaBackend.DatabaseFiles.Tables
{
    public class Venta
    {
        public Venta() { }

        [Key]
        public long ID_Venta { get; set; }

        public int Total { get; set; }
        public DateTime Fecha { get; set; }

        public long ID_Local { get; set; } 

        [ForeignKey("ID_Local")]
        public Local Local { get; set; }

        [InverseProperty("Venta")]
        public List<VentaDetalle> VentaDetalles { get; set; }
    }
}
