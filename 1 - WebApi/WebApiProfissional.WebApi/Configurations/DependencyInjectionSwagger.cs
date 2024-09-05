using WebApiProfissional.CrossCutting.IoC.NativeInjector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class DependencyInjectionSwagger
    {
        /// <summary>
        ///  Essa class serve como um ponto central para a configuração da injeção de dependência na aplicação, 
        ///  configura a geração de documentação Swagger para a API, incluindo detalhes sobre segurança
        ///  relacionados ao uso de tokens JWT (JSON Web Token)
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddInfraStrutureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                c.SwaggerDoc(
                    "webapi-profissional",
                    new OpenApiInfo
                    {
                        Title = "Projeto WebApi Profissional",
                        Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                        Description = "Usando o Swagger para criar a API do WebApiProfissional.",                        
                        Contact = new OpenApiContact
                        {
                            Name = "David Wallace Marques Ferreira",
                            Email = "davidwallacem@hotmail.com"
                        },
                        License = new OpenApiLicense { Name = "The MIT License ®", Url = new Uri("https://opensource.org/licenses/MIT") }
                    });          
            });

            return services;
        }
    }
}
