using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.ConsoleUI.Views
{
    public class Menu
    {
        private readonly AgendaFunctions _funcoesAgenda;
        private readonly Querys _consultas;

        public Menu(AgendaFunctions funcoesAgenda, Querys consultas)
        {
            _funcoesAgenda = funcoesAgenda;
            _consultas = consultas;
        }

        public void MenuIndex()
        {
            do
            {
                Console.Clear();

                Console.WriteLine("---- Menu Agenda ----");
                Console.WriteLine("1 - Agenda");
                Console.WriteLine("2 - Consultas");
                Console.WriteLine("0 - Sair");
                var index = ConsoleInput.ReadNumber();

                if (index == 0)
                    return;

                var menu = new Dictionary<int, Action>();
                menu.Add(1, _funcoesAgenda.AgendaMenu);
                menu.Add(2, _consultas.ConsultaMenu);

                if (menu.ContainsKey(index))
                    menu[index].Invoke();
                else
                    ConsoleInput.InvalidOption();

            } while (true);
        }
    }
}
