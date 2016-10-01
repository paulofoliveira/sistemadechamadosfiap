using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaChamadosFiap.Web.Models
{
    public class AtendenteModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
