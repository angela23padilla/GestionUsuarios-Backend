using GestionUsuarios.Application.DTOs;
using GestionUsuarios.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.Application.Interface
{
    public  interface IPersonaService
    {

        Task<Response<int>> AgregarPersonaAsync(PersonaDto personaDto);

        Task<int> AgregarPersonasAsync(PersonaDto personaDto);
        Task<PaginacionDto<PersonaDto>> ObtenerPersonasPaginadosAsync(int page, int size, string sortBy, string search);
        Task<List<PersonaDto>> ObtenerPersonasAsync(int id);
        Task<bool> ModificarPersonaAsync(int id, PersonaDto productDto);
        Task<bool> EliminarPersonaAsync(int id);




    }
}
