using BookApp.DataLayer;
using BookApp.Interfaces;
using BookApp.Interfaces.IRepository;
using BookApp.Repository;
using BookApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp
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
            var connectionstring = Configuration.GetConnectionString("BookAppConnectionString");
            services.AddDbContext<AppDatabaseContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("BookAppConnectionString")));
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.Preserve);

            var secretKey = Configuration.GetValue<string>("JWT:SecreteKey");
            var issuer = Configuration.GetValue<string>("JWT:Issuer");
            var audience = Configuration.GetValue<string>("JWT:Audience");


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(options =>
{
        options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetValue<string>("JWT:SecreteKey"))),
        ValidIssuer = Configuration.GetValue<string>("JWT:Issuer"),
        ValidAudience = Configuration.GetValue<string>("JWT:Audience"),
        ValidateIssuer = true,
        ValidateAudience = true
    };
});    

            services.AddScoped<IAccountService, AccountService> ();
            services.AddScoped(typeof(IBaseRepository<>),typeof(Repository<>));
            services.AddScoped<IOrder_Interface, OrderService>();
            // services.AddControllers(
            //options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "BookApp", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[]{}
    }
});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseMiddleware<GlobalExceptionHandler>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           // app.Use(async (context, next) => { });
          

        }
    }
}
