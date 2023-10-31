using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefontanaBackend.DatabaseFiles.Tables
{
    public class Marca
    {
        [Key]
        public long ID_Marca { get; set; }

        [Required]
        [StringLength(20)]
        public string Nombre { get; set; } = string.Empty;
    }
}
