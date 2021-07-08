using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIngresosGasto.Models
{
    public class Especialidad
    {
        [Key]
        public int idEspecialidad { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public List<MedicoEspecialidad> MedicoEspecialidad { get; set; }
    }
}
