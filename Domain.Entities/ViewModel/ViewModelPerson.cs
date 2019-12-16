
namespace Domain.Entities.ViewModel
{
    using Domain.Entities.Mantenimiento;
    using PagedList;
    public class ViewModelPerson
    {
        public IPagedList<EPersona> PersonaGrilla { get; set; }
        public EPersona Person { get; set; }

        public EPersona FiltroPerson { get; set; }
        public int? cantGrid { get; set; }
        public int cantPage { get; set; }
        public int cantTotal { get; set; }
        public int pageView { get; set; }
    }
}
