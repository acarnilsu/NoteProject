using AutoMapper;
using Note.EntityLayer.Concrete;
using Note.Web.ViewModels;

namespace Note.Web.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppNote, AppNoteListVM>();
            CreateMap<AppNoteListVM, AppNote>();
        }
    }
}
