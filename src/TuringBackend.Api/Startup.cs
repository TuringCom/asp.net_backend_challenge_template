﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using TuringBackend.Api.Core;
using TuringBackend.Models;
using TuringBackend.Models.Data;
using TokenHandler = TuringBackend.Api.Core.TokenHandler;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace TuringBackend.Api
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("Adding services...");
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenHandler, Core.TokenHandler>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
            services.Configure<AppOptions>(Configuration.GetSection("app"));
            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            // Add services that will be used for CRUD operations
            services.AddCustomServices();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddResponseCaching();

            var connectionString = Configuration.GetConnectionString("TuringBackendDB");
            services.AddDbContext<TuringBackendContext>(c => c.UseMySql(connectionString));

            services.AddCors(o => o.AddPolicy("DefaultPolicy", builder => { builder.AllowAnyOrigin(); }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Turing ECommerce API",
                        Version = "1.2.9",
                        Description = "Official documentation about Turing ECommerce API"
                    });

                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Path.GetFileName(Assembly.GetExecutingAssembly().GetName().Name)}.xml";

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlFile.ToLower());

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"user-key: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }}
                };
                c.AddSecurityRequirement(security);
            });

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = signingConfigurations.Key,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddMvc(options =>
                {
                    options.OutputFormatters.Clear();
                    options.OutputFormatters.Add(new JsonOutputFormatter(new JsonSerializerSettings(),
                        ArrayPool<char>.Shared));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            _logger.LogInformation("Configuring services...");
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseCors("DefaultPolicy");

            app.UseSwagger(c => { c.RouteTemplate = "docs/{documentName}/docs.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/docs/v1/docs.json", "1.2.9");
                c.RoutePrefix = "docs";
            });

            app.ConfigureExceptionHandler(logger);
            app.UseETagger();

            app.UseAuthentication();
            // TODO: app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

 }