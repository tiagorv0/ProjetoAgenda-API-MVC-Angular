using Agenda.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Agenda.MVC.Data
{
    public class ApiBaseReponse<T>
    {
        public T Content { get; set; }
        public IEnumerable<ErrorViewModel> Errors { get; set; } = new List<ErrorViewModel>();
        public bool HasError { get => Errors.Any(); }
        public void AddErrorsToModelState(ModelStateDictionary modelState)
        {
            foreach (var error in Errors)
                modelState.AddModelError(string.Empty, error.ErrorMessage);
        }
    }
}
