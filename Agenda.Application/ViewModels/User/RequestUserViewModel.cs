using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Agenda.Application.ViewModels.User
{
    public class RequestUserViewModel
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int UserRoleId { get; set; }
    }
}
