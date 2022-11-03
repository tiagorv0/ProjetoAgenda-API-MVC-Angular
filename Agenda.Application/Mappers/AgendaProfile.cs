using Agenda.Application.Extensions;
using Agenda.Application.Utils;
using Agenda.Application.ViewModels.AdminContact;
using Agenda.Application.ViewModels.Contact;
using Agenda.Application.ViewModels.Enums;
using Agenda.Application.ViewModels.Interaction;
using Agenda.Application.ViewModels.Phone;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Entities;
using Agenda.Domain.Entities.Enums;
using AutoMapper;

namespace Agenda.Application.Mappers
{
    public class AgendaProfile : Profile
    {
        public AgendaProfile()
        {
            CreateMap<RequestContactViewModel, Contact>()
                .MergeList(x => x.Phones, vm => vm.Phones);
            CreateMap<Contact, ResponseContactViewModel>();

            CreateMap<RequestAdminContactViewModel, Contact>()
                .ForMember(x => x.UserId, m => m.MapFrom(req => req.UserId))
                .MergeList(x => x.Phones, vm => vm.Phones);
            CreateMap<Contact, ResponseAdminContactViewModel>();

            CreateMap<RequestPhoneViewModel, Phone>()
                .ForMember(x => x.DDD, m => m.MapFrom(req => PhoneUtils.Split(req.FormattedPhone).Item1))
                .ForMember(x => x.Number, m => m.MapFrom(req => PhoneUtils.Split(req.FormattedPhone).Item2));
            CreateMap<Phone, ResponsePhoneViewModel>();

            CreateMap<RequestUserViewModel, User>();
            CreateMap<User, ResponseUserViewModel>();

            CreateMap<Interaction, ResponseInteractionViewModel>().ReverseMap();
            CreateMap<PhoneType, PhoneTypeViewModel>().ReverseMap();
            CreateMap<InteractionType, InteractionTypeViewModel>().ReverseMap();
            CreateMap<UserRole, UserRoleViewModel>().ReverseMap();
        }
    }
}
