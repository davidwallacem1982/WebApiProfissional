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

            /// <summary>
            /// Registra a lógica de negócios de funcionários no contêiner de injeção de dependência.
            /// A implementação de <see cref="IFuncionariosLogic"/> será feita pela classe <see cref="FuncionariosLogic"/>.
            /// Esta lógica lida com todas as regras de negócio associadas aos funcionários, como validações,
            /// cálculos e operações específicas de negócio antes de interagir com os repositórios.
            /// </summary>
            services.AddScoped<IFuncionariosLogic, FuncionariosLogic>();

            /// <summary>
            /// Registra a lógica de negócios de usuários no contêiner de injeção de dependência.
            /// A implementação de <see cref="IUsuarioLogic"/> será feita pela classe <see cref="UsuarioLogic"/>.
            /// Esta lógica gerencia todas as regras de negócio relacionadas aos usuários, incluindo autenticação,
            /// permissões, gestão de perfis e outras operações específicas antes de interagir com os repositórios.
            /// </summary>
            services.AddScoped<IUsuarioLogic, UsuarioLogic>();

            #endregion

        }
    }

}