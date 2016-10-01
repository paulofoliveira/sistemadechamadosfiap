using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaChamadosFiap.Web.Models
{
    public class ChamadoModel
    {
        public ChamadoModel()
        {
            Interacoes = new List<ChamadoInteracaoModel>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime DtAbertura { get; set; }
        public IEnumerable<ChamadoInteracaoModel> Interacoes { get; set; }
    }
}
