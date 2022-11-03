using Agenda.Application.Interfaces;
using Agenda.Application.Services;
using Agenda.Application.Token;
using Agenda.Application.Utils;
using Agenda.Application.Validations;
using Agenda.Domain.Entities;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Repositories;
using Agenda.Infrastructure.Storage;
using Agenda.Infrastructure.UnityOfWork;
using FluentValidation.AspNetCore;

namespace Agenda.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IJsonStorage<Interaction>, JsonStorage<Interaction>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAgendaService, AgendaService>();
            services.AddScoped<IAgendaAdminService, AgendaAdminService>();
            services.AddScoped<IInteractionService, InteractionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IInteractionRepository, InteractionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IRulesValidation, RulesValidation>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddFluentValidation(x =>
            {
                x.AutomaticValidationEnabled = false;
                x.RegisterValidatorsFromAssemblyContaining<UserValidator>();
            });

            return services;
        }
    }
}
