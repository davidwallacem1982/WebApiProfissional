using WebApiProfissional.CrossCutting.IoC.NativeInjector;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        /// <summary>
        ///  Essa class serve como um ponto central para a configuração da injeção de dependência na aplicação, 
        ///  encapsulando as chamadas específicas para os métodos RegisterServices dos todas as class 
        ///  NativeInjector. 
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            NativeInjectorInfra.RegisterServices(services);
            NativeInjectorLogic.RegisterServices(services);
            NativeInjectorRepository.RegisterServices(services);
        }
    }
}
