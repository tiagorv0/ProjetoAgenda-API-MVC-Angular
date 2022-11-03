using Agenda.MVC.ViewModels;
using Refit;

namespace Agenda.MVC.Data
{
    public interface IApiLoginService
    {
        [Post("/auth")]
        Task<ApiResponse<TokenViewModel>> Login(LoginViewModel loginViewModel);
    }
}
