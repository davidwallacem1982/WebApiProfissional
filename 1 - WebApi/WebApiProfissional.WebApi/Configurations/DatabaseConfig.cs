using WebApiProfissional.Infra.Configurations.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace WebApiProfissional.WebApi.Configurations
{
    public static class DatabaseConfig
    {
        /// <summary>
        /// Registra o contexto do banco de dados "IntegrationContext" e configurando-o para usar o MySQL
        /// com a string de conexão chamada de "MySQLConnection"
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<IntegrationContext>(options =>
                options.UseMySql(configuration.GetConnectionString("MySQLConnection"),
                     ServerVersion.Parse("8.0.34")));          

        }
    }
}
