namespace EventHorizon.Game.Editor.Server
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseResponseCompression();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles(
                GetStaticFileOptions(
                    env
                )
            );

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }

        private static StaticFileOptions GetStaticFileOptions(
            IWebHostEnvironment env
        )
        {
            var staticConfig = new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    // Expires time set to 15 minutes
                    const int durationInSeconds = 60 * 15;
                    context.Context.Response.Headers[
                        "Cache-Control"
                    ] = "public,max-age=" + durationInSeconds;
                }
            };
            if (env.IsDevelopment())
            {
                staticConfig = new StaticFileOptions
                {
                    OnPrepareResponse = context =>
                    {
                        // Set the Cache Control header to max-age = 0 for development
                        context.Context.Response.Headers[
                            "Cache-Control"
                        ] = "public,max-age=0";
                    }
                };
            }

            return staticConfig;
        }
    }
}
