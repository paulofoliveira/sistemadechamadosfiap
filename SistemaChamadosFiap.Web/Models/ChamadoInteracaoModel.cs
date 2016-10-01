using System;

namespace SistemaChamadosFiap.Web.Models
{
    public class ChamadoInteracaoModel
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public DateTime DtInteracao { get; set; }
    }
}