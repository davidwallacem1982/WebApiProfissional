using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Services;
using WebApiProfissional.Infra.Identity;
using WebApiProfissional.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiProfissional.CrossCutting.IoC.NativeInjector
{
    public static class NativeInjectorInfra
    {
        /// <summary>
        /// Responsável por registrar os serviços relacionados aos serviços que estão na Infra em um 
        /// contêiner de injeção de dependência
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IFuncionariosServices, FuncionariosServices>();

            services.AddScoped<IUsuarioServices, UsuarioServices>();

            #endregion

            #region Identity

            services.AddScoped<IAuthenticate, AuthenticateService>();

            #endregion
        }
    }
}