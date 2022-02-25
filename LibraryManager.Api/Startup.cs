namespace LibraryManager.Api
{
    using LibraryManager.Api.Configurations;
    using LibraryManager.Api.Services;
    using LibraryManager.Domain.Abstractions.Services;
    using LibraryManager.Infrastructure;
    using LibraryManager.Infrastructure.Repositories;
    using LibraryManager.Infrastructure.Repositories.Abstractions;
    using LibraryManager.Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(GetXmlPath());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryManager.Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<LibraryManagerDbContext>(
                options =>
                    options.UseNpgsql(
                        GetConnectionString(),
                        x => x.MigrationsAssembly("LibraryManager.Infrastructure")));


            services.Configure<Settings>(Configuration.GetSection("Settings"));


            var settings = Environment.GetEnvironmentVariable("Settings")
                            ?? Configuration.GetSection("Settings").GetSection("Secret").Value;

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(settings)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration =
            //        Configuration.GetConnectionString("LibraryManagerRedis");
            //    options.InstanceName = "LibraryManagerRedis";
            //});

            // Repositories
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();

            // Services
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEncryptService, EncryptService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryManager.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(MyAllowSpecificOrigins);
            });
        }

        private string GetConnectionString()
        {
            var uriString = Environment.GetEnvironmentVariable("DATABASE_URL")
                                ?? Configuration.GetConnectionString("DATABASE_URL");
            var uri = new Uri(uriString);
            var db = uri.AbsolutePath.Trim('/');
            var user = uri.UserInfo.Split(':')[0];
            var passwd = uri.UserInfo.Split(':')[1];
            var port = uri.Port > 0 ? uri.Port : 5432;
            var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4};SSL Mode=Require;Trust Server Certificate=True;",
                uri.Host, db, user, passwd, port);
            return connStr;
        }

        private string GetXmlPath()
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
            return Path.Combine(basePath, fileName);
        }
    }
}
