using Agenda.Application.Interfaces;
using Agenda.Application.Utils;
using Agenda.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Agenda.Domain.Interfaces;

namespace Agenda.ConsoleUI.Views
{
    public class AgendaFunctions
    {
        private readonly IAgendaService _agendaService;
        private readonly PhoneValidator _phoneValidator;

        private UpdateContactViewModel _contact = new UpdateContactViewModel();

        public AgendaFunctions(IAgendaService contactService, PhoneValidator phoneValidator)
        {
            _agendaService = contactService;
            _phoneValidator = phoneValidator;
        }

        public void AgendaMenu()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("---- Menu Agenda ----");
                Console.WriteLine("1 - Adicionar Contato");
                Console.WriteLine("2 - Atualizar Contato");
                Console.WriteLine("3 - Remover Contato");
                Console.WriteLine("4 - Adicionar Telefone");
                Console.WriteLine("5 - Atualizar Telefone");
                Console.WriteLine("6 - Remover Telefone");
                Console.WriteLine("9 - Salvar dados");
                Console.WriteLine("0 - Voltar\n");

                var option = ConsoleInput.ReadNumber("Selecione uma opção: ");

                if (option == 0)
                {
                    Console.Clear();
                    return;
                }

                var menuContact = new Dictionary<int, Action>();
                menuContact.Add(1, CreateContactView);
                menuContact.Add(2, UpdateContact);
                menuContact.Add(3, RemoveContact);
                menuContact.Add(4, CreatePhoneView);
                menuContact.Add(5, UpdateContact);
                menuContact.Add(6, RemovePhone);
                menuContact.Add(9, SaveAsync);

                if (menuContact.ContainsKey(option))
                    menuContact[option].Invoke();
                else
                    ConsoleInput.InvalidOption();

            } while (true);
        }

        public async void CreateContactView()
        {
            Console.Clear();
            Console.WriteLine("---- Adicionar Contato ----");
            _contact.Name = ConsoleInput.ReadString("Insira um Nome: ");

            var phoneVM = RenderPhone();

            var phones = _contact.Phones.ToList();
            phones.Add(phoneVM);
            _contact.Phones = phones;

            await _agendaService.CreateAsync(_contact);

            Console.ReadKey();
        }

        public void CreatePhoneView()
        {
            Console.Clear();
            Console.WriteLine("---- Adicionar Telefone Ao Contato ----");
            _contact = GetContact();

            var phoneVM = RenderPhone();
            var phones = _contact.Phones.ToList();

            phones.Add(phoneVM);
            _contact.Phones = phones;

            Console.ReadKey();
        }

        public async void RemoveContact()
        {
            Console.Clear();
            Console.WriteLine("---- Remover Contato ----");
            var contactExist = GetContact();

            await _agendaService.RemoveAsync(contactExist.Id);
            Console.ReadKey();
        }

        public void RemovePhone()
        {
            Console.Clear();
            Console.WriteLine("---- Remover Telefone ----");

            _contact = GetContact();

            Console.WriteLine(_contact.ToString());

            var idPhone = ConsoleInput.ReadNumber("Insira o ID do Telefone: ");

            if (!_contact.Phones.Any(x => x.Id == idPhone)) 
            {
                Console.WriteLine("Id não encontrado!");
                Console.ReadKey();
                RemovePhone();
            }
            else
            {
                var phones = _contact.Phones.ToList();
                var phone = phones.FirstOrDefault(x => x.Id == idPhone);
                phones.Remove(phone);
                _contact.Phones = phones;
            }

            Console.ReadKey();
        }

        public void UpdateContact()
        {
            Console.Clear();
            Console.WriteLine("---- Atualizar Contato e/ou Telefone ----\n");

            _contact = GetContact();

            Console.WriteLine(_contact.ToString());

            _contact.Name = ConsoleInput.ReadString("Insira o Nome do contato: ");

            var option = ConsoleInput.ReadYesOrNo("Deseja atualizar o numero de telefone? (Y/n)");

            if (option)
            {
                var phoneUpdated = UpdatePhone();

                var phones = _contact.Phones.ToList();
                var phone = phones.FirstOrDefault(x => x.Id == phoneUpdated.Id);
                var index = phones.IndexOf(phone);

                phones[index] = phoneUpdated;

                Console.ReadKey();
            }
        }

        private async void SaveAsync()
        {
            try
            {
                await _agendaService.UpdateAsync(_contact);
                Console.WriteLine("Dados Salvo!");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private UpdatePhoneViewModel UpdatePhone()
        {
            var idPhone = ConsoleInput.ReadNumber("Insira o ID do Telefone:");

            var phoneVM = RenderPhone();

            phoneVM.Id = idPhone;

            return phoneVM;
        }

        private UpdateContactViewModel GetContact()
        {
            var idContact = ConsoleInput.ReadNumber("Insira o ID do Contato:");

            var contactExist = _agendaService.GetByIdAsync(idContact).GetAwaiter().GetResult();

            if (!(_agendaService.IdExists(idContact).Result))
            {
                Console.WriteLine("Id não encontrado!");
                contactExist = GetContact();
            }

            return contactExist;
        }

        private UpdatePhoneViewModel RenderPhone()
        {
            Console.WriteLine("\n---- Adicionar Telefone ----\n");

            var description = ConsoleInput.ReadString("Insira a Descrição: ");

            var phoneFormatted = ConsoleInput.ReadString("Insira o Número de Telefone ( Ex: (xx) 9?xxxx-xxxx ): ");

            var phoneType = ConsoleInput.ReadNumber("Insira o tipo de telefone (1 - Fixo, 2 - Celular, 3 - Comercial): ");

            var phoneVM = new UpdatePhoneViewModel
            {
                Description = description,
                FormattedPhone = phoneFormatted,
                PhoneTypeId = phoneType
            };

            var verifyPhone = _phoneValidator.VerifyPhoneViewModel(phoneVM).Result;
            if (verifyPhone.Errors.Count > 0)
            {
                foreach (var error in verifyPhone.Errors)
                    Console.WriteLine(error);

                Console.ReadKey();
                phoneVM = RenderPhone();
            }

            Validation.ValidateViewModel(phoneVM);

            return phoneVM;
        }
    }
}
