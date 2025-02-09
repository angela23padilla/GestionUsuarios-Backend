using GestionUsuarios.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.Infraestructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext> _logger;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) : base(options)
        {
            _logger = logger;
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Persona>()
        .Property(p => p.IdentificacionCompleta)
        .HasComputedColumnSql("[TipoIdentificacion] + ' - ' + [NumeroIdentificacion]");

            modelBuilder.Entity<Persona>()
                .Property(p => p.NombreCompleto)
                .HasComputedColumnSql("[Nombres] + ' ' + [Apellidos]");

        }
    }
}
