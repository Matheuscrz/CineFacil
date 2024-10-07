using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Models
{
    /// <summary>
    /// Modelo de administrador.
    /// </summary>
    public class AdministradorModel
    {
        public int Id { get; set; }

        public Cargo Cargo { get; set; }

        public int UserId { get; set; }
    }
}