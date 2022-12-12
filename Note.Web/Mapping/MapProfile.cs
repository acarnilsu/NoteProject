using AutoMapper;
using Note.EntityLayer.Concrete;
using Note.Web.Areas.Manager.ViewModels;
using Note.Web.ViewModels;

namespace Note.Web.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppNote, AppNoteListVM>();
            CreateMap<AppNoteListVM, AppNote>();

            CreateMap<SignInVM, AppUser>();
            CreateMap<AppUser, SignInVM>();

            CreateMap<AppRole, CreateRoleVM>();
            CreateMap<CreateRoleVM, AppRole>();

            CreateMap<AppUser, AdminUserListVM>().ReverseMap();

        }
    }
}
