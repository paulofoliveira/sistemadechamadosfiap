using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaChamadosFiap.Web.Models
{
    /// <summary>
    /// Classe Model de Chamado.
    /// </summary>
    public class ChamadoModel : BaseModel
    {
        public ChamadoModel()
        {
            DtAbertura = DateTime.Now;
            Interacoes = new List<ChamadoInteracaoModel>();
        }

        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }


        [Required]
        [Display(Name = "Abertura")]
        [DataType(DataType.DateTime)]
        public DateTime DtAbertura { get; set; }  

        [Required]
        public byte Status { get; set; }

        [Required]
        public byte Prioridade { get; set; }
        
        [UIHint("Interacoes")]
        public IEnumerable<ChamadoInteracaoModel> Interacoes { get; set; }
    }
}
