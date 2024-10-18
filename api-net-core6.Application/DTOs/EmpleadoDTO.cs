using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net_core6.Application.DTOs
{
    public class EmpleadoDTO
    {
        public int Id { get; set; } // Identificador único del empleado
        public string Nombres { get; set; } // Nombres del empleado
        public string Apellidos { get; set; } // Apellidos del empleado
        public DateTime FechaNacimiento { get; set; } // Fecha de nacimiento
        public DateTime FechaIngreso { get; set; } // Fecha de ingreso a la empresa
        public int IdAFP { get; set; } // Identificador de la AFP
        public string? AFP { get; set; } // Nombre de la AFP
        public int IdCargo { get; set; } // Identificador del cargo
        public string? Cargo { get; set; } // Nombre del cargo
        public decimal Sueldo { get; set; } // Sueldo del empleado
        public bool Status { get; set; } // Estado del empleado (activo/inactivo)
    }
}
