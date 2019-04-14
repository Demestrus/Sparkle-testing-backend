using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
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
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = false,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = false,
                    ValidateTokenReplay = false,
                    ValidateActor = false,
                    //ValidIssuer = Configuration["Jwt:Issuer"],
                    //ValidAudience = Configuration["Jwt:Audience"],
                    TokenReader = (token, param) =>
                    {
                        var decodedToken = FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token).Result;

                        return new JwtSecurityToken(
                        claims: decodedToken.Claims.Select(c => new Claim(c.Key, c.Value.ToString()))
                        );
                    },
                    SignatureValidator = (token, param) =>
                    {
                        var decodedToken = FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token).Result;

                        return new JwtSecurityToken(
                        issuer: decodedToken.Issuer,
                        audience: decodedToken.Audience,
                        claims: decodedToken.Claims.Select(c => new Claim(c.Key, c.Value.ToString())),
                        expires: DateTime.Now.AddTicks(decodedToken.ExpirationTimeSeconds
                        );
                    },
                    TokenReplayValidator = (date, token, param) =>
                    {
                        return true;
                    }
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
                opt.Events = new JwtBearerEvents
                {
                    //OnMessageReceived = async context =>
                    //{
                    //    var token = context.Token;
                    //    if (token != null)
                    //    {
                    //        var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                    //    }

                    //    return Task.CompletedTask;
                    //},
                    OnChallenge = async context =>
                    {
                        var test = 1;
                    },
                    OnTokenValidated = async context =>
                    {
                        var test = 1;
                    },
                    OnAuthenticationFailed = async context =>
                    {
                        var test = 1;
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
