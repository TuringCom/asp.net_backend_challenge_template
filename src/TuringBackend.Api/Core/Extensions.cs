using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using TuringBackend.Api.Services;

namespace TuringBackend.Api.Core
{
    public static partial class Extensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var options = configuration.GetOptions<RedisOptions>("redis");
            services.AddDistributedRedisCache(x => { x.Configuration = options.ConnectionString; });

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IAttributeService, AttributeService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IShippingService, ShippingService>();
            services.AddTransient<ITaxService, TaxService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();
            services.AddTransient<ICreditCardService, CreditCardService>();
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }

        private static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }

        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, string applicationName = null)
        {
            return webHostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var appOptions = context.Configuration.GetOptions<AppOptions>("app");
                var seqOptions = context.Configuration.GetOptions<SeqOptions>("seq");
                var serilogOptions =
                    context.Configuration.GetOptions<SerilogOptions>("serilog");
                if (!Enum.TryParse<LogEventLevel>(serilogOptions.Level, true, out var level))
                    level = LogEventLevel.Information;

                applicationName = string.IsNullOrWhiteSpace(applicationName) ? appOptions.Name : applicationName;
                loggerConfiguration.Enrich.FromLogContext()
                    .MinimumLevel.Is(level)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName);
                Configure(loggerConfiguration, level, seqOptions, serilogOptions);
            });
        }

        private static void Configure(LoggerConfiguration loggerConfiguration, LogEventLevel level,
            SeqOptions seqOptions, SerilogOptions serilogOptions)
        {
            if (seqOptions.Enabled) loggerConfiguration.WriteTo.Seq(seqOptions.Url, apiKey: seqOptions.ApiKey);

            if (serilogOptions.ConsoleEnabled) loggerConfiguration.WriteTo.Console();
        }
    }
}