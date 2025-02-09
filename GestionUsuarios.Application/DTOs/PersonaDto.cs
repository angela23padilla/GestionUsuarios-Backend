using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.Application.DTOs
{
    public class PersonaDto
    {

        public int Id { get; set; }

        [Required]
       
        public string Nombres { get; set; }

        [Required]
       
        public string Apellidos { get; set; }

        [Required]
        
        public string NumeroIdentificacion { get; set; }

        [Required]
       
        [EmailAddress]
        public string Email { get; set; }

        [Required]

        public string TipoIdentificacion { get; set; }

        
    }
}
