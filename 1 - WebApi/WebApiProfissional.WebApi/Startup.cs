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
    /// Classe Startup � a respons�vel pela configura��o dos servi�os e do pipeline de requisi��es HTTP da aplica��o.
    /// Esta classe segue o padr�o de inicializa��o utilizado no ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Construtor que inicializa a configura��o da aplica��o a partir do objeto <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="configuration">Configura��es da aplica��o obtidas a partir de appsettings.json e outras fontes.</param>
        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        /// Objeto <see cref="IConfiguration"/> que armazena as configura��es da aplica��o.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Este m�todo � chamado pelo runtime. Ele � usado para adicionar os servi�os ao cont�iner de inje��o de depend�ncia.
        /// Aqui s�o configurados os servi�os necess�rios para a aplica��o, incluindo MVC, Newtonsoft.Json, SignalR, entre outros.
        /// </summary>
        /// <param name="services">A cole��o de servi�os da aplica��o.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // ====================================== Serialize Newtonsoft ========================================== //
            // Instala os pacotes necess�rios:
            // Microsoft.AspNetCore.Mvc.NewtonsoftJson , Microsoft.AspNetCore.SignalR.Protocols.Newtonsoft

            // Configura��o simples para adicionar suporte ao Newtonsoft.Json na serializa��o/deserializa��o.
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                // Evita refer�ncias circulares durante a serializa��o JSON.
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // Converte enums para string durante a serializa��o.
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            // Configura��o de API da Web com suporte ao Newtonsoft.Json.
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Evita refer�ncias circulares durante a serializa��o JSON.
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // Converte enums para string durante a serializa��o.
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            // Configura��o de SignalR com suporte ao Newtonsoft.Json.
            services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
            {
                // Evita refer�ncias circulares durante a serializa��o JSON no SignalR.
                options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // Converte enums para string durante a serializa��o no SignalR.
                options.PayloadSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
            // ===================================================================================================== //

            // Configura as rotas para utilizar URLs em letras min�sculas.
            services.AddRouting(options => options.LowercaseUrls = true);

            // =================================== Dependency Injection ============================================ //

            // Adiciona a configura��o de autentica��o.
            services.AddAuthenticationConfiguration(Configuration);

            // Adiciona a configura��o do Swagger para documenta��o da API.
            services.AddInfraStrutureSwagger();

            // Adiciona a configura��o do banco de dados.
            services.AddDatabaseConfiguration(Configuration);

            // Adiciona as configura��es de inje��o de depend�ncia customizadas.
            services.AddDependencyInjectionConfiguration();
            // ===================================================================================================== //

            // Registra o servi�o IHttpContextAccessor, permitindo acesso ao HttpContext atual em servi�os ou classes
            // fora do pipeline de controle do ASP.NET Core (como em classes de servi�o ou reposit�rios).
            services.AddHttpContextAccessor();

            //======================================================Swagger============================================================================//
            //Isso aqui � uma outra forma de fazer a documenta�� do Swagger
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
        /// Este m�todo � chamado pelo runtime. Ele � usado para configurar o pipeline de requisi��es HTTP.
        /// Aqui s�o configurados os middlewares, como autentica��o, roteamento, endpoints, etc.
        /// </summary>
        /// <param name="app">Construtor da aplica��o que define os middlewares do pipeline.</param>
        /// <param name="env">Informa��es sobre o ambiente da aplica��o (desenvolvimento, produ��o, etc.).</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Verifica se a aplica��o est� em ambiente de desenvolvimento.
            if (env.IsDevelopment())
            {
                // Exibe a p�gina de exce��o detalhada no ambiente de desenvolvimento.
                app.UseDeveloperExceptionPage();

                // Configura o Swagger para documenta��o interativa da API.
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/webapi-profissional/swagger.json", "WebApiProfissional.WebAPI v1");
                });
            }

            // For�a a aplica��o a redirecionar para HTTPS.
            app.UseHttpsRedirection();

            // Adiciona o roteamento ao pipeline.
            app.UseRouting();

            // Explica��o da ordem: UseAuthentication precede UseAuthorization no pipeline
            // Isso garante que a autentica��o seja executada antes da autoriza��o.
            app.UseAuthentication(); // Necess�rio para usar autentica��o
            app.UseAuthorization();  // Necess�rio para usar autoriza��o

            // Configura os endpoints da aplica��o para mapear controladores.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
