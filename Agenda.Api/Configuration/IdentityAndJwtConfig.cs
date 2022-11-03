using System.Text;
using Agenda.Application.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Agenda.Api.Configuration
{
    public static class IdentityAndJwtConfig
    {
        public static IServiceCollection AddIdentityAndJwtConfiguration(
            this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = configuration["Jwt:Key"];

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(Roles.Admin, policy => policy.RequireRole(Roles.Admin));
                opt.AddPolicy(Roles.Common, policy => policy.RequireRole(Roles.Common));
            });

            return services;
        }
    }
}
