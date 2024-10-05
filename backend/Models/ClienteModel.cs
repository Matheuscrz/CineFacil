using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class ClienteModel : UserModel
    {
        public int Id { get; set; }
        public string Cpf { get; set; }

        public DateTime DataNascimento { get; set; }

        public string telefone { get; set; }

        public int userId { get; set; }
    }
}