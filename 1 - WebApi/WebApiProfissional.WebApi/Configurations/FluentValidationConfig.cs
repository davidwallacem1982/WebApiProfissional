using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApiProfissional.Domain.Validation.Usuario;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddValidatorsFromAssemblyContaining<NewUsuarioInputValidator>();
        }
    }
}
