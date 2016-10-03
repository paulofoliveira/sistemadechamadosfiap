using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace SistemaChamadosFiap.Web.Models
{
    /// <summary>
    /// Classe Model de interação do chamado.
    /// </summary>
    public class ChamadoInteracaoModel : BaseModel
    {
        public ChamadoInteracaoModel()
        {
            DtInteracao = DateTime.Now;
            Usuario = ClaimsPrincipal.Current.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Role).Value;
        }

        [Required]        
        [DataType(DataType.MultilineText)]
        public string Mensagem { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DtInteracao { get; set; }

        [Required]
        public string Usuario { get; set; }
    }
}