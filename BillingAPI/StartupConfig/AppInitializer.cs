using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BillingAPI.StartupConfig
{
    /// <summary>
    /// App initializer
    /// </summary>
    public static class AppInitializer
    {
        /// <summary>
        /// Initialize application, using spa, static files, configure routing, configure swagger etc
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Initialize(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureSwagger();
            app.ConfigureRoutingAndCors();
            app.UseSession();
            app.ConfigureEndpoints(env);
        }

        private static void ConfigureRoutingAndCors(this IApplicationBuilder app)
        {
            app.UseRouting();
        }

        private static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Billing API");
            });
        }

        private static void ConfigureEndpoints(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");


            });
        }
    }
}
