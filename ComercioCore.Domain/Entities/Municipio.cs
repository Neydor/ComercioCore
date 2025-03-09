using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComercioCore.Domain.Entities
{
        public class Municipio
        {
            public int Id { get; set; }

            [Required]
            [StringLength(100)]
            public string Nombre { get; set; }
        }
}
