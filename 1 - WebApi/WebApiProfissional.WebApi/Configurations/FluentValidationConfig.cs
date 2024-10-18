using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApiProfissional.Domain.Validation.Funcionario;
using WebApiProfissional.Domain.Validation.Usuarios;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class FluentValidationConfig
    {
        /// <summary>
        /// Configura o FluentValidation para o projeto, registrando todos os validadores
        /// presentes nos assemblies que contêm <see cref="RegisterFuncionarioValidator"/> e 
        /// <see cref="RegisterUsuarioValidator"/>.
        /// </summary>
        /// <param name="services">A coleção de serviços onde os validadores serão registrados.</param>
        /// <exception cref="ArgumentNullException">Lançado quando a coleção de serviços é <c>null</c>.</exception>
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddValidatorsFromAssemblyContaining<RegisterFuncionarioValidator>();
            services.AddValidatorsFromAssemblyContaining<RegisterUsuarioValidator>();
        }

    }
}
