using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SparkleTesting.Application.Services;
using SparkleTesting.Domain.Entities;
using SparkleTesting.Persistence;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using AutoMapper.Configuration.Conventions;

namespace SparkleTesting.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(b => b.AddPolicy("CorsPolicy", builder =>
              {
                  builder.SetIsOriginAllowed(s => true);
                  builder.AllowAnyHeader();
                  builder.AllowCredentials();
                  builder.AllowAnyMethod();
              }));

            services.AddDbContext<SparkleDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString(("Default")), 
                b => b.MigrationsAssembly("SparkleTesting.Persistence")));

            services.AddScoped<Initializer>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SparkleDbContext>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt => {
                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = async context =>
                    {
                        var authorization = context.Request.Headers["Authorization"].ToString();

                        if (string.IsNullOrEmpty(authorization))
                        {
                            context.NoResult();
                            return;
                        }

                        if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            var token = authorization.Substring("Bearer ".Length).Trim();

                            if (string.IsNullOrEmpty(token))
                            {
                                context.NoResult();
                                return;
                            }

                            var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                            context.Principal = new ClaimsPrincipal(
                                new ClaimsIdentity(
                                    decodedToken.Claims.Select(c => new Claim(c.Key, c.Value.ToString()))
                                    .Append(new Claim(ClaimsIdentity.DefaultNameClaimType, decodedToken.Uid)), 
                                    JwtBearerDefaults.AuthenticationScheme
                                ));

                            context.Success();
                        }

                        return;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Fail(context.Exception);
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    options.SubstituteApiVersionInUrl = true;
                });


            services.AddSwaggerGen(
                options =>
                {
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, new Info()
                        {
                            Title =
                                $"{GetType().Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product}",
                            Version = description.ApiVersion.ToString(),
                            Description = description.IsDeprecated ? "DEPRECATED" : ""

                        });
                    }

                    options.OperationFilter<SwaggerDefaultValues>();

                    options.IncludeXmlComments(Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        $"{GetType().Assembly.GetName().Name}.xml"
                    ));

                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Введите в поле JWT токен с припиской Bearer",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                    options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                       { "Bearer", new string[] { } }
                    });

                });

            services.AddAutoMapper();

            services.AddScoped<UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.GetApplicationDefault()
            });

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(
            options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    var alias = string.IsNullOrWhiteSpace(Configuration["Swagger:Alias"]) ? "" : $"/{Configuration["Swagger:Alias"]}";

                    options.SwaggerEndpoint($"{alias}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
