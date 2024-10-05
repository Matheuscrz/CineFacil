using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Enums;

namespace backend.Models
{
    public class AdministradorModel
    {
        public int Id { get; set; }

        public Cargo Cargo { get; set; }

        public int userId { get; set; }
    }
}