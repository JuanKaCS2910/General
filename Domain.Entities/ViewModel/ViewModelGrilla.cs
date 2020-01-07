using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewModel
{
    public class ViewModelGrilla
    {
        public int? cantGrid { get; set; }
        public int cantPage { get; set; }
        public int cantTotal { get; set; }
        public int pageView { get; set; }
        public string Url { get; set; }
    }
}
