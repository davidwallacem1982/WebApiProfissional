using WebApiProfissional.WebApi.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace WebApiProfissional.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //===========================================Serialize Newsoft=============================================================================//
            //Instalar os nugets:
            //Microsoft.AspNetCore.Mvc.NewtonsoftJson , Microsoft.AspNetCore.SignalR.Protocols.Newtonsoft

            //simples:
            services.AddMvc().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            //API da web simples
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

            //SignalR
            services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
            {
                options.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.PayloadSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });
            //=========================================================================================================================================//

            //Deixa as Routes em letra minuscula.
            services.AddRouting(options => options.LowercaseUrls = true);

            //======================================================Dependency Injection===============================================================//

            //Adicionando AuthenticationConfiguration
            services.AddAuthenticationConfiguration(Configuration);

            //Adicionando InfraStrutureSwagger
            services.AddInfraStrutureSwagger();

            //Adicionando DatabaseConfiguration
            services.AddDatabaseConfiguration(Configuration);

            //Adicionando DependencyInjectionConfiguration
            services.AddDependencyInjectionConfiguration();

            //=========================================================================================================================================//

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/webapi-profissional/swagger.json", "WebApiProfissional.WebAPI v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            //O arranjo UseAuthentication precede UseAuthorization na configura��o do pipeline ASP.NET Core por uma raz�o
            //estrat�gica: a autentica��o � uma etapa preliminar essencial que estabelece a identidade do usu�rio, sendo
            //um requisito pr�vio para a autoriza��o.Portanto, a ordem precisa ser mantida para garantir que o sistema
            //primeiro autentique o usu�rio antes de avaliar e conceder as permiss�es necess�rias para acessar a API.
            //Este procedimento segue as pr�ticas recomendadas de seguran�a, proporcionando uma abordagem sequencial
            //coerente no fluxo do pipeline, onde a autentica��o precede a autoriza��o.
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
