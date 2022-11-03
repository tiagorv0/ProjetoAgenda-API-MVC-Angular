using FluentValidation.Results;

namespace Agenda.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();

        public BadRequestException() : base("Requisição inválida por favor verifique os dados e tente novamente.")
        {
        }

        public BadRequestException(string prop, string message) : this()
        {
            Errors.Add(new ValidationFailure(prop, message));
        }

        public BadRequestException(ValidationResult validationResult) : this()
        {
            Errors.AddRange(validationResult.Errors);
        }
    }
}
