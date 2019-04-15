using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SparkleTesting.Application.Services;
using SparkleTesting.Domain;
using SparkleTesting.Persistence;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SparkleTesting
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

            services.AddDbContext<SparkleDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString(("Default")), 
                b => b.MigrationsAssembly("SparkleTesting.Persistence")));

            services.AddScoped<Initializer>();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SparkleDbContext>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
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
                                    decodedToken.Claims.Select(c => new Claim(c.Key, c.Value.ToString())), 
                                    JwtBearerDefaults.AuthenticationScheme
                                ));

                            context.Success();
                        }

                        return;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        //TODO ловить исключения туть
                        return Task.FromException(context.Exception);
                    }
                };
            });

            services.AddScoped<UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
