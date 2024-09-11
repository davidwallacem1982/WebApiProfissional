using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Validation.Usuarios;

namespace WebApiProfissional.CrossCutting.IoC.NativeInjector
{
    public static class NativeInjectorValidator
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region UsuariosController

            services.AddScoped<IValidator<NewUsuarioInput>, NewUsuarioInputValidator>();

            #endregion

        }
    }
}
