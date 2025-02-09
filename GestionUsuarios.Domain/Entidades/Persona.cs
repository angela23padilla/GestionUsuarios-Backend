using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.Domain.Entidades
{
    public class Persona
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; }

        [Required]
        [MaxLength(20)]
        public string NumeroIdentificacion { get; set; }

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string TipoIdentificacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; } = DateTime.UtcNow;


        public string Estado { get; set; }

        public string IdentificacionCompleta { get; private set; }
        public string NombreCompleto { get; private set; }
    }
}
