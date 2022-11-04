using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NetCore.AutoRegisterDi;
using BillingAPI.Enums;
using BillingAPI.Extensions;
using BillingAPI.BillingService.Interfaces;
using BillingAPI.BillingService.FileGenerators;

namespace BillingAPI.StartupConfig
{

    /// <summary>
    /// Service Initializer
    /// </summary>
    public static class ServicesInitializer
    {
        private static readonly Assembly _apiAssembly = AppDomain.CurrentDomain.Load("BillingAPI");
        private static readonly Assembly _billingServiceAssembly = AppDomain.CurrentDomain.Load("BillingAPI.BillingService");

        /// <summary>
        /// Initialize services, adding dependencies, register db context, register swagger etc
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Initialize(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();
            services.AddControllers();
            services.AddMediator();
            services.RegisterDependencies();
            services.AddHttpContextAccessor();
            services.AddHttpContextAccessor();
        }

        private static void AddSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(opt => opt.SetSwaggerOptions());

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(_apiAssembly);
            services.AddMediatR(_billingServiceAssembly);
        }


        private static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddMediatR(_apiAssembly);
            services.AddMediatR(_billingServiceAssembly);
            services.RegisterPublicImplementedServices();
        }
        private static void RegisterPublicImplementedServices(this IServiceCollection services)
        {
            services.RegisterAssemblyPublicNonGenericClasses(new[] { _apiAssembly, _billingServiceAssembly })
                .Where(c => c.Name.EndsWith(DependenciesSignature.Service.GetEnumStringName()) ||
                            c.Name.EndsWith(DependenciesSignature.Repository.GetEnumStringName()) ||
                            c.Name.EndsWith(DependenciesSignature.Reader.GetEnumStringName()))
                .AsPublicImplementedInterfaces();
        }

    }
}
