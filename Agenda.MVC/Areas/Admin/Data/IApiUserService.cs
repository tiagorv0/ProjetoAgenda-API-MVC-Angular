using Agenda.MVC.Areas.Admin.ViewModels;
using Agenda.MVC.Data;
using Agenda.MVC.ViewModels;
using Refit;

namespace Agenda.MVC.Areas.Admin.Data
{
    [Headers("Authorization: Bearer")]
    public interface IApiUserService
    {
        [Get("/admin/user/{id}")]
        Task<UserGetViewModel> GetUserByIdAsync(int id);

        [Get("/admin/user/search")]
        Task<IEnumerable<UserGetViewModel>> GetAllUsersAsync();

        [Get("/admin/user/user-roles")]
        Task<IEnumerable<EnumerationViewModel>> GetUserRolesAsync();

        [Post("/admin/user")]
        Task<ApiBaseReponse<UserGetViewModel>> CreateUserAsync(UserPostViewModel viewModel);

        [Put("/admin/user/{id}")]
        Task<ApiBaseReponse<UserGetViewModel>> UpdateUserAsync(int id, UserPostViewModel viewModel);

        [Delete("/admin/user/{id}")]
        Task RemoveUserAsync(int id);
    }
}
