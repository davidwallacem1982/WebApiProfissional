using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class DependencyInjectionSwagger
    {
        /// <summary>
        /// Configura o Swagger para a aplicação, adicionando definições de segurança e documentação.
        /// </summary>
        /// <param name="services">A coleção de serviços onde o Swagger será adicionado.</param>
        /// <returns>A coleção de serviços com o Swagger configurado.</returns>
        public static IServiceCollection AddInfraStrutureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Configura a definição de segurança para a autenticação JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Adiciona a exigência de segurança para as APIs
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

                // Configura as informações da documentação do Swagger
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
