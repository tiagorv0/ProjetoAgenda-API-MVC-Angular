using Agenda.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Agenda.Infrastructure.Utils
{
    public static class PasswordHasher
    {
        public static void PasswordHash(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
        }

        public static bool ValidPasswordAsync(User user, string modelPassword)
        {
            if (modelPassword == null && user.Password == null)
                return false;

            var passwordHasher = new PasswordHasher<User>();
            var status = passwordHasher.VerifyHashedPassword(user, user.Password, modelPassword);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;

                case PasswordVerificationResult.Success:
                    return true;

                default:
                    throw new ArgumentNullException("Erro na validação da senha !");
            }
        }
    }
}
