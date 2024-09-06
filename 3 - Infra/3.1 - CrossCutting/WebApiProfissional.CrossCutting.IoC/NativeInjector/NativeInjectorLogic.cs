using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Services.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiProfissional.CrossCutting.IoC.NativeInjector
{
    public static class NativeInjectorLogic
    {
        /// <summary>
        /// Método responsável por registrar as classes de lógica de negócio (services) 
        /// no contêiner de injeção de dependência. Cada classe de lógica é registrada com o 
        /// tempo de vida Scoped, garantindo que cada solicitação tenha sua própria instância dos serviços.
        /// </summary>
        /// <param name="services">O contêiner de serviços onde as lógicas de negócio serão registradas.</param>
        public static void RegisterServices(IServiceCollection services)
        {
            #region Logic

            // Adiciona a lógica de negócios para funcionários ao contêiner de injeção de dependência.
            // Esta lógica irá lidar com as regras específicas de negócio relacionadas aos funcionários.
            services.AddScoped<IFuncionariosLogic, FuncionariosLogic>();

            // Adiciona a lógica de negócios para usuários ao contêiner de injeção de dependência.
            // A lógica será responsável pelas regras de negócio específicas relacionadas aos usuários.
            services.AddScoped<IUsuarioLogic, UsuarioLogic>();

            #endregion
        }
    }

}