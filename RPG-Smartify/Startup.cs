using AutoMapper;
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
using RPG_Smartify.Data;
using RPG_Smartify.Service.CharacterService;
using RPG_Smartify.Service.CharacterSkill;
using RPG_Smartify.Service.WeaponService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Smartify
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
            services.AddDbContext<RPGdbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            if(Configuration.GetSection("Environment:env").Value.Equals("Development"))
            {
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("RPGV1",
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "RPG API DOC",
                            Version = "1",
                            Description = "Documenting the API for RPG",
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                            {
                                Email = "Kishor.jabegu5@gmail.com",
                                Name = "Kishor Jabegu",
                                Url = new Uri("https://www.kjabs5.com")
                            }
                        });
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme." +
                        "Enter 'Bearer' and then the token" +
                        "Example = Bearer 123tokenXYZ...",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"



                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
                    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var commentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                    options.IncludeXmlComments(commentsFullPath);

                });
            }
          

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<ICharacterService, CharacterService>();

            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddScoped<IWeaponRepo, WeaponService>();

            services.AddScoped<ICharacterSkillRepo, CharacterSkillService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey= new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer=false,
                        ValidateAudience=false

                };
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("swagger/RPGV1/swagger.json", "RPG Web API");
                    options.RoutePrefix = "";

                });
            }
            if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
               
            }

           app.UseHttpsRedirection();
           
            app.UseRouting();

            app.UseCors(x=>x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
