
using System.ComponentModel.DataAnnotations;
using Agenda.Application.ViewModels.Enums;
using Agenda.Domain.Core;

namespace Agenda.Application.ViewModels.User
{
    public class ResponseUserViewModel : Register
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int UserRoleId { get; set; }
    }
}
