namespace Agenda.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Id não encontrado")
        {

        }
    }
}
