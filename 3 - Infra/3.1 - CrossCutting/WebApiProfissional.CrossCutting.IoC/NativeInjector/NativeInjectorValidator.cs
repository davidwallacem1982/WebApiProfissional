using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebApiProfissional.Domain.InputModels.Funcionarios;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Validation.Funcionario;
using WebApiProfissional.Domain.Validation.Usuarios;

namespace WebApiProfissional.CrossCutting.IoC.NativeInjector
{
    public static class NativeInjectorValidator
    {
        /// <summary>
        /// Registra os serviços e validadores no contêiner de injeção de dependências.
        /// </summary>
        /// <param name="services">A coleção de serviços <see cref="IServiceCollection"/> onde os validadores serão registrados.</param>
        public static void RegisterServices(IServiceCollection services)
        {
            #region FuncionariosController

            // Registra o validador para NewFuncionarioInput como um serviço com escopo
            services.AddScoped<IValidator<NewFuncionarioInput>, RegisterFuncionarioValidator>();

            #endregion

            #region UsuariosController

            // Registra o validador para NewUsuarioInput como um serviço com escopo
            services.AddScoped<IValidator<NewUsuarioInput>, RegisterUsuarioValidator>();

            #endregion
        }

    }
}
