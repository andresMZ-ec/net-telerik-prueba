using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClaseNegocio
{
    public class CrearActualizarUsuarioDto
    {
        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Contrasena { get; set; } = string.Empty;
    }

    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombresCompletos { get; set; }
        public string NombreUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public bool Activo { get; set; }
    }
}
