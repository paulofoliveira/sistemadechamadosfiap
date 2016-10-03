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
        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
