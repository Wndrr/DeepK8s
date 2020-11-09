using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using BlazorStrap;
using CurrieTechnologies.Razor.Clipboard;
using Desktop.Components.UserInterface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Desktop.Services;
using ElectronNET.API;
using ElectronNET.API.Entities;
using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Http;
using Stl.DependencyInjection;
using Stl.Fusion;

namespace Desktop
{
    public class Startup
    {
        private bool _hasStartupFailed = false;
        private string? _startupFailureMessage = null;

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


            try
            {
                KubernetesClientConfiguration.LoadKubeConfig();
            }
            catch (Exception e)
            {
                _hasStartupFailed = true;
                _startupFailureMessage = e.ToString();
            }

            if (_hasStartupFailed)
                return;

            services.AddTransient(provider =>
            {
                var configFile = KubernetesClientConfiguration.LoadKubeConfig();

                configFile.CurrentContext =
                    InMemoryUserPreferencesStore.CurrentContextName ?? configFile.CurrentContext;
                var config = KubernetesClientConfiguration.BuildConfigFromConfigObject(configFile);

                return new Kubernetes(config);
            });
            services.AddSingleton<KubernetesHelper>();
            services.AddSingleton<EntityReferenceUrlBuilder>();
            services.AddSingleton<KubernetesCommandLineBuilder>();
            services.AddBlazoredSessionStorage();
            services.AddClipboard();
        
            services.AddBootstrapCss();
            var fusion = services.AddFusion();
            services.AddSingleton(c => new UpdateDelayer.Options() {
                // Default update delayer options 
                Delay = TimeSpan.FromSeconds(0.1),
            });
            RegisterFusionDb(fusion);
            services.AttributeBased().AddServicesFrom(Assembly.GetExecutingAssembly());
        }

        private static void RegisterFusionDb(FusionBuilder fusion)
        {
            
            var listTypeDefinition = typeof(IKubernetesObject<V1ListMeta>);
            var listTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => listTypeDefinition.IsAssignableFrom(p))
                .ToList();
            
            var typeDefinition = typeof(IKubernetesObject<V1ObjectMeta>);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeDefinition.IsAssignableFrom(p))
                .ToList();
            
            foreach (var listType in listTypes)
            {
                try
                {
                    var selectedSingleType = types.Single(type => listType.Name.Replace("List", "") == type.Name);
                    var serviceType = typeof(KubernetesRepository<,>);
                    var typeToAdd = serviceType.MakeGenericType(listType, selectedSingleType);
                    fusion.AddComputeService(typeToAdd);
                }
                catch (Exception)
                {
                    // ignored
                }
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

            if (_hasStartupFailed)
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/",
                        async context =>
                        {
                            var startupFailureMessage = "Application startup failure. Invalid config. Kubeconfig file could not be parsed.";
                            startupFailureMessage += "\r\n" + _startupFailureMessage;
                            await context.Response.WriteAsync(startupFailureMessage);
                        });
                });
            }
            else
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapBlazorHub();
                    endpoints.MapFallbackToPage("/_Host");
                });
            }

            Task.Run(async () => await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions()
            {
                Title = "DeepK8s",
            }));
        }
    }
}