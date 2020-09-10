using System.Security.Claims;
using CardApplication.Domain.Repositories;
using CardApplication.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CardApplication.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static void AddAuth0Services(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = $"https://{configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = configuration["Auth0:ApiIdentifier"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
            
            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("write:card",
            //         policy => policy.Requirements.Add(new HasScopeRequirement("write:card", domain)));
            // });
            //
            // services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }

        public static void AddCardApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ICreditCardRepository, CreditCardRepository>();
        }
    }
}