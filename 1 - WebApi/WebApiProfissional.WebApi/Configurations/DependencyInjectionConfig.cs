using WebApiProfissional.CrossCutting.IoC.NativeInjector;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Configura a injeção de dependência para a aplicação, registrando serviços de infraestrutura, lógica e repositórios.
        /// </summary>
        /// <param name="services">A coleção de serviços onde os serviços serão registrados.</param>
        /// <exception cref="ArgumentNullException">Lançado quando a coleção de serviços é nula.</exception>
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            NativeInjectorInfra.RegisterServices(services);
            NativeInjectorLogic.RegisterServices(services);
            NativeInjectorRepository.RegisterServices(services);
        }
    }

}
