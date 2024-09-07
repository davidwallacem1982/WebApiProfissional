using WebApiProfissional.WebApi.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace WebApiProfissional.WebApi
{
    /// <summary>
    /// Classe Startup é a responsável pela configuração dos serviços e do pipeline de requisições HTTP da aplicação.
    /// Esta classe segue o padrão de inicialização utilizado no ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Construtor que inicializa a configuração da aplicação a partir do objeto <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="configuration">Configurações da aplicação obtidas a partir de appsettings.json e outras fontes.</param>
        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        /// Objeto <see cref="IConfiguration"/> que armazena as configurações da aplicação.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Este método é chamado pelo runtime. Ele é usado para adicionar os serviços ao contêiner de injeção de dependência.
        /// Aqui são configurados os serviços necessários para a aplicação, incluindo MVC, Newtonsoft.Json, SignalR, entre outros.
        /// </summary>
        /// <param name="services">A coleção de serviços da aplicação.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // ====================================== Serialize Newtonsoft ========================================== //
            // Instala os pacotes necessários:
            // Microsoft.AspNetCore.Mvc.NewtonsoftJson , Microsoft.AspNetCore.SignalR.Protocols.Newtonsoft

            // Configuração simples para adicionar suporte ao Newtonsoft.Json na serialização/deserialização.
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                // Evita referências circulares durante a serialização JSON.
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // Converte enums para string durante a serialização.
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            // Configuração de API da Web com suporte ao Newtonsoft.Json.
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Evita referências circulares durante a serialização JSON.
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // Converte enums para string durante a serialização.
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            // Configuração de SignalR com suporte ao Newtonsoft.Json.
            services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
            {
                // Evita referências circulares durante a serialização JSON no SignalR.
                options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // Converte enums para string durante a serialização no SignalR.
                options.PayloadSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
            // ===================================================================================================== //

            // Configura as rotas para utilizar URLs em letras minúsculas.
            services.AddRouting(options => options.LowercaseUrls = true);

            // =================================== Dependency Injection ============================================ //

            // Adiciona a configuração de autenticação.
            services.AddAuthenticationConfiguration(Configuration);

            // Adiciona a configuração do Swagger para documentação da API.
            services.AddInfraStrutureSwagger();

            // Adiciona a configuração do banco de dados.
            services.AddDatabaseConfiguration(Configuration);

            // Adiciona as configurações de injeção de dependência customizadas.
            services.AddDependencyInjectionConfiguration();
            // ===================================================================================================== //

            // Registra o serviço IHttpContextAccessor, permitindo acesso ao HttpContext atual em serviços ou classes
            // fora do pipeline de controle do ASP.NET Core (como em classes de serviço ou repositórios).
            services.AddHttpContextAccessor();

            //======================================================Swagger============================================================================//
            //Isso aqui é uma outra forma de fazer a documentaçã do Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc(
            //        "integration-webapi",
            //        new OpenApiInfo
            //        {
            //            Title = "Projeto Integration",
            //            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
            //            Description = "Usando o Swagger para criar a API do WebApiProfissional.",
            //            Contact = new OpenApiContact
            //            {
            //                Name = "David Wallace Marques Ferreira",
            //                Email = "davidwallacem@hotmail.com"
            //            }
            //        });
            //});
            //=========================================================================================================================================//
        }

        /// <summary>
        /// Este método é chamado pelo runtime. Ele é usado para configurar o pipeline de requisições HTTP.
        /// Aqui são configurados os middlewares, como autenticação, roteamento, endpoints, etc.
        /// </summary>
        /// <param name="app">Construtor da aplicação que define os middlewares do pipeline.</param>
        /// <param name="env">Informações sobre o ambiente da aplicação (desenvolvimento, produção, etc.).</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Verifica se a aplicação está em ambiente de desenvolvimento.
            if (env.IsDevelopment())
            {
                // Exibe a página de exceção detalhada no ambiente de desenvolvimento.
                app.UseDeveloperExceptionPage();

                // Configura o Swagger para documentação interativa da API.
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/webapi-profissional/swagger.json", "WebApiProfissional.WebAPI v1");
                });
            }

            // Força a aplicação a redirecionar para HTTPS.
            app.UseHttpsRedirection();

            // Adiciona o roteamento ao pipeline.
            app.UseRouting();

            // Explicação da ordem: UseAuthentication precede UseAuthorization no pipeline
            // Isso garante que a autenticação seja executada antes da autorização.
            app.UseAuthentication(); // Necessário para usar autenticação
            app.UseAuthorization();  // Necessário para usar autorização

            // Configura os endpoints da aplicação para mapear controladores.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
