using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiProfissional.CrossCutting.IoC.NativeInjector
{
    public static class NativeInjectorRepository
    {
        /// <summary>
        /// Responsável por registrar os serviços relacionados ao Repository em um 
        /// contêiner de injeção de dependência
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services)
        {
            #region Domain

            services.AddScoped<IFuncionariosRepository, FuncionariosRepository>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped<IRevokedTokenRepository, RevokedTokenRepository>();

            #endregion
        }
    }
}