using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using NotificationCenter.Data;
using NotificationCenter.Inputs.Authorization.ApiKeyQueryImpl;
using NotificationCenter.Inputs.Authorization.Models;
using NotificationCenter.Inputs.Authorization.Services;
using NotificationCenter.PayloadParser;
using NotificationCenter.PayloadParser.PayloadParserImpl;
using NotificationCenter.PayloadParser.PayloadSearchEngineImpl;
using NotificationCenter.Provider.Processor;
using PayloadParser;

namespace NotificationCenter
{
    public class Startup
    {

        private static Engine engine;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddDbContext<DataContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddApiKeySupport(options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ApiKeyAccess, policy => policy.Requirements.Add(new OnlyApiKeyAuthorizationRequirement()));
            });

            services.AddScoped<IGetApiKeyQuery, InDBApiKeyQuery>();
            services.AddSingleton<IAuthorizationHandler, OnlyApiKeyAuthorizationHandler>();

            services.AddHttpContextAccessor();

            using(DataContext db = new DataContext())
            {
                db.Database.EnsureCreated();

                if (!db.Inputs.Any())
                {
                    db.Mappings.Add(new Mapping()
                    {
                        Output = new Output() {
                            Schema = "{'test':'${thierry.toto}'}",
                            Provider = new OutputProvider() { 
                                OutputParameters = new List<OutputParameter>(),
                                ProviderUrl = "",
                                Type = OutputType.Rest
                            }
                        },
                        Input = new Input()
                        {
                            Schema = "",
                            Provider = new InputProvider()
                            {
                                InputParameters = new List<InputParameter>() { new InputParameter() { ParameterKey = "MQTTTopic", ParameterValue = "test/notification" } },
                                Type = InputType.Mqtt,
                                ProviderUrl = "10.1.2.5"
                            }
                        }
                    });
                    db.SaveChanges();
                }

                engine = new Engine(db.Inputs.Include(i => i.Provider).ThenInclude(p => p.InputParameters).ToList());
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
