using GestionUsuarios.Application.DTOs;
using GestionUsuarios.Application.Interface;
using GestionUsuarios.Application.Model;
using GestionUsuarios.Domain.Entidades;
using GestionUsuarios.Infraestructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GestionUsuarios.Infraestructure.Service
{
    public class PersonaService : IPersonaService
    {
        private readonly ApplicationDbContext _context;

        public PersonaService(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<int> AgregarPersonasAsync(PersonaDto persona)
        {
            try
            {

                var per = new Persona
                {
                    Nombres = persona.Nombres,
                    Apellidos = persona.Apellidos,
                    NumeroIdentificacion = persona.NumeroIdentificacion,
                    Email = persona.Email,
                    TipoIdentificacion = persona.TipoIdentificacion,
                    FechaCreacion = DateTime.UtcNow,
                    Estado = "A"
                };


                await _context.Personas.AddAsync(per);
                await _context.SaveChangesAsync();

                return persona.Id;

            }
            catch (Exception e)
            {
                return 0;
            }

        }

        public async Task<PaginacionDto<PersonaDto>> ObtenerPersonasPaginadosAsync(int page, int size, string sortBy, string search)
        {
            var query = _context.Personas
                .Where(m => m.Estado == "A")
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.Nombres.Contains(search) ||
                    p.Apellidos.Contains(search)
                    );
            }

            query = sortBy.ToLower() switch
            {

                "date" => query.OrderByDescending(p => p.FechaCreacion),
                "nombres" => query.OrderBy(p => p.Nombres),
                _ => query.OrderBy(p => p.Nombres),
            };

            var totalRecords = await query.CountAsync();

            var personas = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            var productoDtos = personas.Select(p => new PersonaDto
            {
                Id = p.Id,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                NumeroIdentificacion = p.NumeroIdentificacion,
                Email = p.Email,
                TipoIdentificacion = p.TipoIdentificacion

            }).ToList();

            return new PaginacionDto<PersonaDto>
            {
                TotalRegistros = totalRecords,
                Items = productoDtos
            };
        }


        public async Task<List<PersonaDto>> ObtenerPersonasAsync(int id)
        {
            var personas = new List<PersonaDto>();

            try
            {
                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("sp_Persona", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@PersonaID", id == 0 ? (object)DBNull.Value : id));

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync()) // Leer múltiples registros
                            {
                                var personaDto = new PersonaDto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                                    NumeroIdentificacion = reader.GetString(reader.GetOrdinal("NumeroIdentificacion")),
                                    Email = reader.GetString(reader.GetOrdinal("Email")),
                                    TipoIdentificacion = reader.GetString(reader.GetOrdinal("TipoIdentificacion"))
                                };

                                personas.Add(personaDto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las personas: {ex.Message}", ex);
            }

            return personas; // Retornar la lista de personas
        }









        public async Task<Response<int>> AgregarPersonaAsync(PersonaDto personaDto)
        {
            try
            {
                _context.Database.OpenConnection();
                // Parámetros de entrada
                var parametros = new[]
                {
            new SqlParameter("@Nombres", personaDto.Nombres),
            new SqlParameter("@Apellidos", personaDto.Apellidos),
            new SqlParameter("@NumeroIdentificacion", personaDto.NumeroIdentificacion),
            new SqlParameter("@Email", personaDto.Email),
            new SqlParameter("@TipoIdentificacion", personaDto.TipoIdentificacion),
            new SqlParameter("@Opcion", 1),

            // Parámetros de salida
            new SqlParameter("@Valido", SqlDbType.Int) { Direction = ParameterDirection.Output },
            new SqlParameter("@Mensaje", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output }
        };

                // Ejecutar el procedimiento almacenado
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_Persona @Nombres, @Apellidos, @NumeroIdentificacion, @Email, @TipoIdentificacion, @Opcion, @Valido OUTPUT, @Mensaje OUTPUT",
                    parametros
                );

                // Leer los valores de salida
                int valido = (int)parametros[6].Value;  // @Valido
                string mensaje = parametros[7].Value.ToString();  // @Mensaje

                bool exito = valido == 1;

                _context.Database.CloseConnection();
                return new Response<int>(exito, mensaje, valido);
            }
            catch (Exception ex)
            {
                _context.Database.CloseConnection();
                return new Response<int>(false, $"Error al agregar persona: {ex.Message}", 0);
            }
        }


        public async Task<bool> ModificarPersonaAsync(int id, PersonaDto personaDto)
        {
            try
            {

                var persona = await _context.Personas.FindAsync(id);

                if (persona == null)
                    return false;


                persona.Nombres = personaDto.Nombres;
                persona.Apellidos = personaDto.Apellidos;
                persona.NumeroIdentificacion = personaDto.NumeroIdentificacion;
                persona.Email = personaDto.Email;
                persona.TipoIdentificacion = personaDto.TipoIdentificacion;
                persona.FechaModificacion = DateTime.Now;


                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }


        }


        public async Task<bool> EliminarPersonaAsync(int id)
        {
            var persona = await _context.Personas.FindAsync(id);

            if (persona == null)
                return false;

            persona.Estado = "I";
            persona.FechaModificacion = DateTime.UtcNow;

            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
