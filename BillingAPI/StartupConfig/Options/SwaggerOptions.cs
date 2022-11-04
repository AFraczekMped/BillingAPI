using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BillingAPI.StartupConfig
{
    public static class SwaggerOptions
    {
        public static void SetSwaggerOptions(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "BillingAPI", Version = "v1" });
            options.AddXmlDocs();
        }

        private static void AddXmlDocs(this SwaggerGenOptions options)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();

            Array.ForEach(xmlDocs, (doc) => options.IncludeXmlComments(doc));
        }
    }
}
