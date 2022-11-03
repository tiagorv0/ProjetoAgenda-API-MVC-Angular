using Agenda.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda.ConsoleUI.Views
{
    public class Querys
    {
        private readonly IAgendaService _agendaService;

        public Querys(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        public void ConsultaMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("---- Consultas ----");
                Console.WriteLine("1 - Consultar todos os Contatos");
                Console.WriteLine("2 - Consultar Contato por Id");
                Console.WriteLine("3 - Consultar Contato por Nome");
                Console.WriteLine("4 - Consultar todos os Telefones");
                Console.WriteLine("5 - Consultar Telefone por Número");
                Console.WriteLine("6 - Consultar Telefone por DDD");
                Console.WriteLine("0 - Voltar\n");

                var option = ConsoleInput.ReadNumber("Selecione uma opção: ");

                if (option == 0)
                {
                    Console.Clear();
                    return;
                }

                var menuPhone = new Dictionary<int, Action>();
                menuPhone.Add(1, GetAllContactView);
                menuPhone.Add(2, GetContactById);
                menuPhone.Add(3, GetContactByName);
                menuPhone.Add(4, GetAllPhoneView);
                menuPhone.Add(5, GetPhoneByNumber);
                menuPhone.Add(6, GetPhoneByDDD);

                if (menuPhone.ContainsKey(option))
                    menuPhone[option].Invoke();
                else
                    ConsoleInput.InvalidOption();

            } while (true);
        }

        public void GetAllContactView()
        {
            Console.Clear();
            Console.WriteLine("---- Listar Todos os Contatos ----");

            var contacts = _agendaService.GetAllAsync().GetAwaiter().GetResult();
            foreach (var contact in contacts)
            {
                Console.WriteLine(contact.ToString());
                Console.WriteLine("=====================");
            }

            Console.ReadKey();
        }

        public void GetContactById()
        {
            Console.Clear();
            Console.WriteLine("---- Listar Contato Por ID ----");
            var id = ConsoleInput.ReadNumber("Insira o ID do Contato: ");

            var contact = _agendaService.GetByIdAsync(id).GetAwaiter().GetResult();

            Console.WriteLine(contact.ToString());

            Console.ReadKey();
        }

        public void GetContactByName()
        {
            Console.Clear();
            Console.WriteLine("---- Listar Contato Por Nome ----");
            var name = ConsoleInput.ReadString("Insira um nome: ");

            var contacts = _agendaService.GetAllAsync(c => c.Name.Contains(name)).GetAwaiter().GetResult();

            foreach (var contact in contacts)
            {
                Console.WriteLine(contact.ToString());
                Console.WriteLine("=====================");
            }

            Console.ReadKey();
        }

        public void GetAllPhoneView()
        {
            Console.Clear();
            Console.WriteLine("---- Listar Todos os Telefones ----");
            var phones =  _agendaService.GetAllAsync().GetAwaiter().GetResult();

            foreach (var phone in phones.SelectMany(x => x.Phones))
            {
                Console.WriteLine(phone.ToString());
                Console.WriteLine("=====================");
            }

            Console.ReadKey();
        }

        public void GetPhoneByDDD()
        {
            Console.Clear();
            Console.WriteLine("---- Listar Telefone Por DDD ----");
            var ddd = ConsoleInput.ReadNumber("Insira um DDD: ");

            var phones = _agendaService.GetAllAsync(x => x.Phones.Any(p => p.DDD == ddd)).GetAwaiter().GetResult();

            foreach (var phone in phones.SelectMany(x => x.Phones))
            {
                Console.WriteLine(phone.ToString());
                Console.WriteLine("=====================");
            }

            Console.ReadKey();
        }

        public void GetPhoneByNumber()
        {
            Console.Clear();
            Console.WriteLine("---- Listar Telefone Por Número ----");
            var number = ConsoleInput.ReadString("Insira um telefone sem DDD: ");

            var contacts = _agendaService.GetAllAsync(x => x.Phones.Any(p => p.Number == number))
                                         .GetAwaiter()
                                         .GetResult();
            var phone = contacts.SelectMany(x => x.Phones).FirstOrDefault();
            Console.WriteLine(phone.ToString());

            Console.ReadKey();
        }
    }
}
