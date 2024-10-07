using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    /// <summary>
    /// Modelo de cliente.
    /// </summary>
    public class ClienteModel : UserModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [StringLength(15)]
        public string Telefone { get; set; }

        public int UserId { get; set; }
    }
}