using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarios.Application.Model
{
    public class Response<T>
    {
        public bool Valido { get; set; }
        public string Mensaje { get; set; }
        public T Datos { get; set; }


        public Response(bool valido, string mensaje, T datos)
        {
            Valido = valido;
            Mensaje = mensaje;
            Datos = datos;
        }
    }
}
