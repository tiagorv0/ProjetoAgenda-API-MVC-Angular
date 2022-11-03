using Microsoft.OpenApi.Models;

namespace Agenda.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Agenda.API",
                    Version = "v1",
                    Description = "Agenda API realizado com FluentValidation, LinqKit, AutoMapper e JWT Bearer Token",
                    Contact = new OpenApiContact
                    {
                        Name = "Tiago Vazzoller",
                        Email = "tiago_rv0@hotmail.com",
                        Url = new Uri("https://tiagovazzoller.com.br")

                    },
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                                    Digite 'Bearer' [espaço] e o token de login.
                                    \r\n\r\nExemplo: 'Bearer 12345abcdef'",

                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
