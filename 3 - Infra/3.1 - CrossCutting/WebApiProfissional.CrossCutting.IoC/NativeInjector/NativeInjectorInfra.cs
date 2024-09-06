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
    /// Método responsável por registrar os serviços de infraestrutura no contêiner de injeção 
    /// de dependência. Serviços de infraestrutura incluem classes que implementam a comunicação
    /// com APIs externas, serviços, entre outros, além de serviços relacionados à autenticação.
    /// </summary>
    /// <param name="services">O contêiner de serviços onde os serviços de infraestrutura serão registrados.</param>
    public static void RegisterServices(IServiceCollection services)
    {
        #region Services

        // Adiciona o serviço relacionado a funcionários no contêiner de injeção de dependência.
        // Este serviço será responsável pela comunicação entre a lógica de negócios e as operações de infraestrutura, 
        // como a comunicação com APIs externas ou manipulação de dados externos.
        services.AddScoped<IFuncionariosServices, FuncionariosServices>();

        // Adiciona o serviço relacionado a usuários no contêiner de injeção de dependência.
        // Assim como o serviço de funcionários, ele é responsável por intermediar entre a lógica de negócios 
        // e as operações de infraestrutura para usuários.
        services.AddScoped<IUsuarioServices, UsuarioServices>();

        #endregion

        #region Identity

        // Adiciona o serviço de autenticação no contêiner de injeção de dependência.
        // Este serviço será responsável por toda a lógica de autenticação e autorização do sistema,
        // implementando a interface IAuthenticate, que define as operações relacionadas à autenticação.
        services.AddScoped<IAuthenticate, AuthenticateService>();

        #endregion
    }
}

}