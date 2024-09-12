using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Services;
using WebApiProfissional.Infra.Services;
using Microsoft.Extensions.DependencyInjection;
using WebApiProfissional.Infra.Services.Identity;

namespace WebApiProfissional.CrossCutting.IoC.NativeInjector
{
    public static class NativeInjectorInfra
{
    /// <summary>
    /// Método responsável por registrar os serviços de infraestrutura no contêiner de injeção 
    /// de dependência. Serviços de infraestrutura incluem classes que implementam a comunicação
    /// com APIs externas, serviços, entre outros, além de serviços relacionados à autenticação.
    /// </summary>
    /// <param name="services">O contêiner de serviços onde os serviços de infraestrutura serão registrados.</param>
    public static void RegisterServices(IServiceCollection services)
    {
            #region Services

            /// <summary>
            /// Registra o serviço de funcionários no contêiner de injeção de dependência.
            /// <see cref="IFuncionariosServices"/> será implementado pela classe <see cref="FuncionariosServices"/>.
            /// Esse serviço é responsável pela intermediação entre a lógica de negócios de funcionários
            /// e as operações de infraestrutura, como a comunicação com APIs externas ou manipulação de dados externos.
            /// </summary>
            services.AddScoped<IFuncionariosServices, FuncionariosServices>();


            services.AddScoped<IRefreshTokenServices, RefreshTokenServices>();

            /// <summary>
            /// Registra o serviço de usuários no contêiner de injeção de dependência.
            /// <see cref="IUsuarioServices"/> será implementado pela classe <see cref="UsuarioServices"/>.
            /// Similar ao serviço de funcionários, esse serviço é responsável pela intermediação entre a lógica de negócios 
            /// de usuários e as operações de infraestrutura, como manipulação de dados ou comunicação com outros serviços.
            /// </summary>
            services.AddScoped<IUsuarioServices, UsuarioServices>();


            #endregion

            #region Identity

            /// <summary>
            /// Registra o serviço de autenticação no contêiner de injeção de dependência.
            /// <see cref="IAuthenticateService"/> será implementado pela classe <see cref="AuthenticateService"/>.
            /// Este serviço gerencia a lógica de autenticação e autorização, incluindo a validação de credenciais e a 
            /// geração de tokens JWT usados no sistema.
            /// </summary>
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            /// <summary>
            /// Registra a implementação da interface <see cref="IAuthorized"/> como um serviço de escopo (<see cref="ServiceLifetime.Scoped"/>).
            /// A classe <see cref="Authorized"/> será injetada sempre que <see cref="IAuthorized"/> for solicitado.
            /// </summary>
            /// <remarks>
            /// O tempo de vida Scoped significa que uma nova instância de <see cref="Authorized"/> será criada para cada solicitação HTTP.
            /// Isso garante que os dados relacionados ao contexto do usuário, como claims de autenticação, sejam gerenciados de forma correta em cada requisição.
            /// </remarks>
            services.AddScoped<IAuthorized, Authorized>();

            #endregion
    }
}

}