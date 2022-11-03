namespace Agenda.Application.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("Usuário sem as permissões adequadas")
        {

        }
    }
}
