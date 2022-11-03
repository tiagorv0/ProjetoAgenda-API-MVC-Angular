using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Agenda.Domain.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Params
{
    public class UserParams : BaseParams<User>
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public override Expression<Func<User, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<User>();

            if (!string.IsNullOrEmpty(Name))
                predicate = predicate.And(x => EF.Functions.Like(x.Name, $"%{Name}%"));

            if (!string.IsNullOrEmpty(UserName))
                predicate = predicate.And(x => EF.Functions.Like(x.UserName, $"%{UserName}%"));

            if (!string.IsNullOrEmpty(Email))
                predicate = predicate.And(x => EF.Functions.Like(x.Email, $"%{Email}%"));

            if (predicate.IsStarted)
                return predicate;
            else
                return null;
        }

        public UserParams()
        {

        }
    }
}
