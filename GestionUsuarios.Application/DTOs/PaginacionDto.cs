using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.Application.DTOs
{
    public class PaginacionDto<T>
    {
        public int TotalRegistros { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
