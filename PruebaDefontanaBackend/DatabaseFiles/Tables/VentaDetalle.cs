using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefontanaBackend.DatabaseFiles.Tables
{
    public class VentaDetalle
    {
        [Key]
        public long ID_VentaDetalle { get; set; }

        public long ID_Venta { get; set; }

        [ForeignKey("ID_Venta")]
        public Venta Venta { get; set; }

        public int Precio_Unitario { get; set; }
        public int Cantidad { get; set; }
        public int TotalLinea { get; set; } 

        public long ID_Producto { get;}
        [ForeignKey("ID_Producto")]
        public Producto Producto { get; set; }


    



    }
}
