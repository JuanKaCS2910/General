﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.General
{
    public class ECampoAuditoria
    {
        public string Usuariocreacion { get; set; }
        public DateTime Fechacreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public DateTime? Fechamodificacion { get; set; }
    }
}
