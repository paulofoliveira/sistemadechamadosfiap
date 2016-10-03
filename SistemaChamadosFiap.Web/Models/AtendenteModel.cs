using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaChamadosFiap.Web.Models
{
    public class AtendenteModel : BaseModel
    {
        public AtendenteModel()
        {
            Ativo = true;
        }

        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
