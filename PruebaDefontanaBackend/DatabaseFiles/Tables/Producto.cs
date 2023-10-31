using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefontanaBackend.DatabaseFiles.Tables
{
    public class Producto
    {
        [Key]
        public long ID_Producto { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; } = string.Empty;


        [Required]
        [StringLength(20)]
        public string Codigo { get; set; } = string.Empty;


        public long ID_Marca { get; set; }
        [ForeignKey("ID_Marca")]
        public Marca Marca { get; set; } 

        [Required]
        [StringLength(20)]
        public string Modelo { get; set; } = string.Empty;

        public int Costo_Unitario { get; set; }


    }
}
