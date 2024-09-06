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
        /// Configura a autenticação para a aplicação usando JWT (JSON Web Token).
        /// </summary>
        /// <param name="services">A coleção de serviços onde a configuração de autenticação será registrada.</param>
        /// <param name="configuration">A configuração da aplicação que fornece as informações necessárias para configurar o JWT.</param>
        /// <exception cref="ArgumentNullException">Lançado quando a coleção de serviços é nula.</exception>
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

                // Configura uma mensagem de erro personalizada quando o usuário não está autorizado
                option.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // Intercepta a resposta de erro padrão
                        context.HandleResponse();

                        // Cria uma mensagem de erro personalizada
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
