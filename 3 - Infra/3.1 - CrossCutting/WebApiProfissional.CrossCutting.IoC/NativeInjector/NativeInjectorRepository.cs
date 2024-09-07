using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiProfissional.CrossCutting.IoC.NativeInjector
{
    public static class NativeInjectorRepository
    {
        /// <summary>
        /// Método responsável por registrar os repositórios relacionados ao domínio da aplicação
        /// no contêiner de injeção de dependência. Cada repositório é registrado com o tempo de vida 
        /// Scoped, garantindo que cada solicitação tenha sua própria instância dos repositórios.
        /// </summary>
        /// <param name="services">O contêiner de serviços onde os repositórios serão registrados.</param>
        public static void RegisterServices(IServiceCollection services)
        {
            #region Domain

            /// <summary>
            /// Registra o repositório de funcionários no contêiner de injeção de dependência.
            /// O repositório <see cref="IFuncionariosRepository"/> será implementado pela classe <see cref="FuncionariosRepository"/>.
            /// A configuração <c>Scoped</c> garante que uma nova instância do repositório seja criada para cada solicitação HTTP.
            /// Este repositório gerencia todas as operações relacionadas à persistência de dados de funcionários.
            /// </summary>
            services.AddScoped<IFuncionariosRepository, FuncionariosRepository>();

            /// <summary>
            /// Registra o repositório de usuários no contêiner de injeção de dependência.
            /// O repositório <see cref="IUsuarioRepository"/> será implementado pela classe <see cref="UsuarioRepository"/>.
            /// Ele é responsável por gerenciar as operações de persistência relacionadas aos usuários.
            /// </summary>
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            /// <summary>
            /// Registra o repositório de tokens de atualização (Refresh Tokens) no contêiner de injeção de dependência.
            /// O repositório <see cref="IRefreshTokenRepository"/> será implementado pela classe <see cref="RefreshTokenRepository"/>.
            /// Esse repositório armazena e gerencia os tokens de atualização gerados para os usuários.
            /// </summary>
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            /// <summary>
            /// Registra o repositório de tokens revogados no contêiner de injeção de dependência.
            /// O repositório <see cref="IRevokedTokenRepository"/> será implementado pela classe <see cref="RevokedTokenRepository"/>.
            /// Ele é utilizado para gerenciar os tokens revogados, impedindo que tokens inválidos ou revogados sejam reutilizados no sistema.
            /// </summary>
            services.AddScoped<IRevokedTokenRepository, RevokedTokenRepository>();

            #endregion

        }
    }

}