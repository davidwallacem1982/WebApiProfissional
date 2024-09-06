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

            // Adiciona o repositório de funcionários ao contêiner de injeção de dependência.
            // Scoped garante que uma nova instância seja criada para cada solicitação HTTP.
            services.AddScoped<IFuncionariosRepository, FuncionariosRepository>();

            // Adiciona o repositório de usuários ao contêiner de injeção de dependência.
            // Este repositório será utilizado para lidar com as operações de persistência de usuários.
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Adiciona o repositório de tokens de atualização (Refresh Tokens) ao contêiner.
            // Este repositório será usado para armazenar e gerenciar tokens de atualização.
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            // Adiciona o repositório de tokens revogados ao contêiner de injeção de dependência.
            // Este repositório gerencia os tokens revogados para evitar o uso de tokens inválidos.
            services.AddScoped<IRevokedTokenRepository, RevokedTokenRepository>();

            #endregion
        }
    }

}