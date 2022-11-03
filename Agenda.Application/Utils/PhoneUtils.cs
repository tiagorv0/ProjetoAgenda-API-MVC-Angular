using System.Text.RegularExpressions;

namespace Agenda.Application.Utils
{
    public class PhoneUtils
    {
        public static bool IsValid(string formattedPhone)
        {
            return new Regex(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$").IsMatch(formattedPhone);
        }

        public static (int, string) Split(string formattedPhone)
        {
            var matchNumber = new Regex(@"(\d+)").Matches(formattedPhone);
            if (matchNumber.Count != 3)
                throw new ArgumentException("Número de telefone inválido");

            return (Convert.ToInt32(matchNumber[0].Value), (matchNumber[1].Value + matchNumber[2].Value));
        }
    }

    public class CellPhone
    {
        public const int TamanhoNumero = 15;
    }

    public class Residencial
    {
        public const int TamanhoNumero = 14;
    }
}
