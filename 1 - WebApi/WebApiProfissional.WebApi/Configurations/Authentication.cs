using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Text.Json;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class Authentication
    {
        /// <summary>
        /// Registra o contexto do banco de dados "IntegrationContext" e configurando-o para usar o MySQL
        /// com a string de conexão chamada de "MySQLConnection"
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
                //Essa linha abaixo gerar mensagem de erro personalizada para o usuário, quer dizer que ele não está logado
                option.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // Intercepta a resposta de erro padrão
                        context.HandleResponse();

                        // Crie uma mensagem de erro personalizada
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var result = JsonSerializer.Serialize(new
                        {
                            message = "Você não está autorizado a acessar este recurso."
                        });

                        return context.Response.WriteAsync(result);
                    }
                };
            });
        }
    }
}
