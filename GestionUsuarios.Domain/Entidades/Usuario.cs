using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.Domain.Entidades
{
    public class Usuario
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [MaxLength(255)]
        public string Pass { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
