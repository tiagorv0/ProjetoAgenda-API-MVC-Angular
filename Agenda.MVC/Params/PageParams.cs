using Agenda.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Params
{
    public class PageParams<T>
    {
        public SearchViewModel Search { get; set; } = new SearchViewModel();
        public int TotalItens { get; private set; }
        public int ItensPerPage { get; private set; } = 5;
        public int CurrentPage { get; private set; }
        public IEnumerable<T> Itens { get; private set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItens / ItensPerPage); }
        }

        public PageParams()
        {
        }

        private PageParams(IEnumerable<T> itens, int totalItens, int itensPerPage, int currentPage)
        {
            Itens = itens;
            TotalItens = totalItens;
            ItensPerPage = itensPerPage;
            CurrentPage = currentPage;
        }

        public PageParams<T> RealizePagination(IEnumerable<T> context, int page)
        {
            var contacts = context;
            var totalContacts = contacts.Count();
            var contactsPerPage = contacts.Skip((page - 1) * ItensPerPage).Take(ItensPerPage).ToList();

            return new PageParams<T>(contactsPerPage, totalContacts, ItensPerPage, page);
        }

        public IEnumerable<SelectListItem> GetSearchProps()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem("Nome", "Name"),
                new SelectListItem("Número", "Number"),
                new SelectListItem("DDD", "DDD")
            };
        }

        public object QueryContactToRefit()
        {
            var query = new object();
            if (Search.Prop == "Name")
                query = new { Name = Search.Value };
            if (Search.Prop == "Number")
                query = new { Number = Search.Value };
            if (Search.Prop == "DDD")
                query = new { DDD = Search.Value };

            return query;
        }
    }
}
