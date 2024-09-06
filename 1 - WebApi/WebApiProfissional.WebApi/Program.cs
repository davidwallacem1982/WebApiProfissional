using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace WebApiProfissional.WebApi
{
    /// <summary>
    /// Classe principal que configura e inicializa o aplicativo ASP.NET Core.
    /// O método Main é o ponto de entrada do aplicativo, responsável por configurar o logging, construir e executar o host web.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Ponto de entrada principal da aplicação. Configura o logger Serilog, carrega as configurações do appsettings.json
        /// e inicia o host web.
        /// </summary>
        /// <param name="args">Argumentos da linha de comando.</param>
        /// <returns>Retorna 0 para execução bem-sucedida, ou 1 se ocorrer uma falha durante a inicialização.</returns>
        public static int Main(string[] args)
        {
            // Cria um objeto de configuração baseado no arquivo appsettings.json.
            var appSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Configura o logger Serilog para registrar eventos com níveis de log específicos.
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()  // Nível mínimo de log: Debug.
                .MinimumLevel.Verbose()  // Nível mínimo de log: Verbose (detalhado).
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)  // Substitui o nível de log para logs da Microsoft.
                .Enrich.FromLogContext()  // Adiciona informações de contexto ao log.
                .WriteTo.Console()  // Escreve logs no console.
                .WriteTo.MySQL(
                    connectionString: appSettings.GetConnectionString("MySQLConnection"),  // Conecta-se ao banco de dados MySQL.
                    tableName: "LogEventsIntegration")  // Registra logs na tabela "LogEventsIntegration".
                .CreateLogger();  // Cria o logger configurado.

            try
            {
                // Loga uma mensagem de informação ao iniciar o host.
                Log.Information("Starting web host");

                // Cria e executa o host web.
                CreateHostBuilder(args).Build().Run();
                return 0;  // Retorna 0 indicando sucesso.
            }
            catch (Exception ex)
            {
                // Loga um erro fatal caso ocorra uma exceção durante a inicialização.
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;  // Retorna 1 indicando falha.
            }
            finally
            {
                // Libera os recursos do logger e fecha qualquer processo pendente de log.
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Configura o host web para a aplicação ASP.NET Core. Define Serilog como o provedor de log
        /// e especifica a classe Startup para configurar os serviços e o pipeline de requisição.
        /// </summary>
        /// <param name="args">Argumentos da linha de comando.</param>
        /// <returns>Um objeto <see cref="IHostBuilder"/> configurado.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()  // Usa Serilog como o provedor de logging para a aplicação.
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Especifica a classe Startup para configurar os serviços e middlewares da aplicação.
                    webBuilder.UseStartup<Startup>();
                });
    }

}
