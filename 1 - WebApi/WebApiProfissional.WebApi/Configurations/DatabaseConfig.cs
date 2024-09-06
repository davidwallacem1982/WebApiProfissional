using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApiProfissional.Infra.Configurations.Contexts;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class DatabaseConfig
    {
        /// <summary>
        /// Configura o contexto de banco de dados para a aplicação, registrando o <see cref="IntegrationContext"/> com a conexão MySQL.
        /// </summary>
        /// <param name="services">A coleção de serviços onde o contexto de banco de dados será registrado.</param>
        /// <param name="configuration">A configuração da aplicação que fornece a string de conexão do banco de dados.</param>
        /// <exception cref="ArgumentNullException">Lançado quando a coleção de serviços é nula.</exception>
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<IntegrationContext>(options =>
                options.UseMySql(configuration.GetConnectionString("MySQLConnection"),
                     ServerVersion.Parse("8.0.34")));
        }
    }

}
