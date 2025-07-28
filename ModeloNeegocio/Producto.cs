using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ModeloNeegocio
{
    [Table("productos")]
    class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public decimal PrecioUnitario { get; set; }

        public int Stock { get; set; }

        public bool Activo { get; set; } = true;

        public ICollection<DetalleOrden> DetallesOrden { get; set; } = new List<DetalleOrden>();
    }
}
