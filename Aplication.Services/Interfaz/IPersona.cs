﻿using Domain.Entities.General;
using Domain.Entities.Mantenimiento;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Interfaz
{
    public interface IPersona
    {
        IPagedList<EPersona> PersonaGrillaToPageList(Grilla pag);
        string CreatePerson(EPersona registro);
        string EditPerson(EPersona registro);
        string DeletePerson(EPersona registro);
        List<EPersona> PersonaGrilla(int? personId);
    }
}