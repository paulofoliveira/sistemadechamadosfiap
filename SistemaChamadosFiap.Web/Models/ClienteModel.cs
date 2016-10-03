﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaChamadosFiap.Web.Models
{
    public class ClienteModel : BaseModel
    {
        [Required]
        public string Nome { get; set; }
    }
}
