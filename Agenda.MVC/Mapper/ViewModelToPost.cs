using Agenda.MVC.Areas.Admin.ViewModels;
using Agenda.MVC.ViewModels;
using AutoMapper;

namespace Agenda.MVC.Mapper
{
    public class ViewModelToPost : Profile
    {
        public ViewModelToPost()
        {
            CreateMap<UserGetViewModel, UserPostViewModel>().ReverseMap();
            CreateMap<ContactGetViewModel, ContactPostViewModel>().ReverseMap();
            CreateMap<ContactAdminGetViewModel, ContactAdminPostViewModel>().ReverseMap();
            CreateMap<PhoneGetViewModel, PhonePostViewModel>().ReverseMap();
        }
    }
}
