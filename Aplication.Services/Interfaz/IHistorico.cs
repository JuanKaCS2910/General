﻿using Domain.Entities.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Interfaz
{
    public interface IHistorico 
    {
        string CreateHistory(EHistorico registro);
    }
}