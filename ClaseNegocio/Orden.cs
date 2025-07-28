using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClaseNegocio
{
    public class CreacionOrdenDto
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "La orden debe tener al menos un elemento en su detalle")]
        public List<DetalleOrdenCreacionDto> Detalle { get; set; } = new List<DetalleOrdenCreacionDto>();
    }

    public class DetalleOrdenCreacionDto
    {
        [Required]
        public int ProductoId { get; set; }

        [Required]
        [Range(1, 1000000)]
        public int Cantidad { get; set; }
    }

    public class OrdenDto
    {
        public int Id { get; set; }
        public DateTime FechaOrden { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public decimal Estado { get; set; }
        public List<DetalleOrdenDto> Detalle { get; set; } = new List<DetalleOrdenDto>();
    }

    public class DetalleOrdenDto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
