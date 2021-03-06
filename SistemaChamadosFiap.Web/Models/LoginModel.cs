﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaChamadosFiap.Web.Models
{
    /// <summary>
    /// Classe model de Login.
    /// </summary>
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
