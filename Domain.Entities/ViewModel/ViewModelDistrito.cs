using Domain.Entities.Mantenimiento;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewModel
{
    public class ViewModelDistrito
    {
        public string Url { get; set; }

        public IPagedList<EDistritoView> DistritoGrilla { get; set; }

        public EDistritoView Distrito { get; set; }
    }
}
