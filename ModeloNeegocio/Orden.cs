using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeloNeegocio
{
    [Table("ordenes")]
    class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public DateTime FechaOrden { get; set; } = DateTime.Now;

        public decimal Total { get; set; }

        public string Estado { get; set; } = "Pendiente";

        public Int64 UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        public ICollection<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>();
    }
}
