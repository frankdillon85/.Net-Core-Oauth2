using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Data.Concrete;
using TaskManager.Data.DB.Models;
using TaskManager.Data.Interface;
using TaskManager.Data.Models.Settings;
using TaskManager.Models;
using TaskManager.Service.Concrete;
using TaskManager.Service.Interface;

namespace TaskManager
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
            services.AddCors();
            services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));
            ConfigureJwtAuthService(services);
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidateModelAttribute());
            });

            services.AddDbContext<JPVDBContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("TaskManagerDB")));

            SetUpDependencies(services);           
        }

        private void SetUpDependencies(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureJwtAuthService(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("AppSettings");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!  
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim  
                ValidateIssuer = false,
                //ValidIssuer = audienceConfig["Iss"],

                // Validate the JWT Audience (aud) claim  
                ValidateAudience = false,

                // Validate the token expiry  
                ValidateLifetime = true,

                ClockSkew = TimeSpan.FromMinutes(5),
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}
