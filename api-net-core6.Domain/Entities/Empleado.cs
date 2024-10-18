using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net_core6.Domain.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime Fecha_Nacimiento { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
        public int Id_AFP { get; set; }
        public string AFP { get; set; }
        public int Id_Cargo { get; set; }
        public string Cargo { get; set; }
        public decimal Sueldo { get; set; }
        public bool Status { get; set; } = true; // Activo por defecto
    }
}
