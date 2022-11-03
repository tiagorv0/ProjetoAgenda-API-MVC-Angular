using System;

namespace Agenda.ConsoleUI.Views
{
    public class ConsoleInput
    {
        public static string ReadString(string message = null)
        {
            if (!string.IsNullOrEmpty(message))
                Console.Write(message);

            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Por favor, insira algum valor");
                input = ReadString();
            }
            return input;
        }

        public static int ReadNumber(string message = null)
        {
            if (!string.IsNullOrEmpty(message))
                Console.Write(message);

            var input = Console.ReadLine();

            int result;
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Por favor, insira algum valor");
                result = ReadNumber();
            }
            else if (!int.TryParse(input, out result))
            {
                Console.WriteLine("Por favor, insira um valor numérico");
                result = ReadNumber();
            }
            return result;
        }

        public static bool ReadYesOrNo(string message = null)
        {
            if (!string.IsNullOrEmpty(message))
                Console.Write(message);

            var input = Console.ReadLine();
            return string.IsNullOrEmpty(input) || input.ToLower().Equals("y");
        }

        public static string ReadNullableString(string field, string message = null)
        {
            if (!string.IsNullOrEmpty(message))
                Console.Write(message);

            var input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? field : input;
        }

        public static void InvalidOption()
        {
            Console.Clear();
            Console.WriteLine("Opção Inválida!");
        }
    }
}
