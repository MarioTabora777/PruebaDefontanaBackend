using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefontanaBackend.DatabaseFiles.Tables
{
    public class Local
    {
        [Key]
        public long ID_Local { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Direccion { get; set; } = string.Empty; 


    }
}
