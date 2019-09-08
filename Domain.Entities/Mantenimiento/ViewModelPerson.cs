
namespace Domain.Entities.Mantenimiento
{
    using PagedList;
    public class ViewModelPerson
    {
        public IPagedList<EPersona> PersonaGrilla { get; set; }
        public EPersona Person { get; set; }
    }
}
