using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Services.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiProfissional.CrossCutting.IoC.NativeInjector
{
    public static class NativeInjectorLogic
    {
        /// <summary>
        /// Responsável por registrar os serviços relacionados à lógica dos serviços em um 
        /// contêiner de injeção de dependência
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            #region Logic

            services.AddScoped<IFuncionariosLogic, FuncionariosLogic>();

            services.AddScoped<IUsuarioLogic, UsuarioLogic>();

            #endregion
        }
    }
}