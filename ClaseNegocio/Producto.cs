using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ClaseNegocio
{
    public class PayloadProductoDto
    {
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, 99999999.99)]
        public decimal PrecioUnitario { get; set; }

        [Required]
        [Range(0,1000000)]
        public decimal Stock { get; set; }
    }

    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
