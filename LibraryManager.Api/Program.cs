using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});

var corsOriginsEnv = Environment.GetEnvironmentVariable("CORS_ORIGINS");
var allowedOrigins = (corsOriginsEnv ?? "https://librarymanager-web.herokuapp.com,https://librarymanager-web-staging.herokuapp.com")
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("SpaPolicy", p =>
    {
        p.WithOrigins(allowedOrigins)
         .AllowAnyHeader()
         .AllowAnyMethod();
    });
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddSwaggerGen(c =>
{
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

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

// Database context
builder.Services.AddDbContext<LibraryManagerDbContext>(
    options =>
        options.UseNpgsql(
            GetConnectionString(builder.Configuration),
            x => x.MigrationsAssembly("LibraryManager.Infrastructure")));

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

var settings = Environment.GetEnvironmentVariable("Settings")
                ?? builder.Configuration.GetSection("Settings").GetSection("Secret").Value;

builder.Services
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
                Encoding.ASCII.GetBytes(settings!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Repositories
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();
builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();

// Services
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IEncryptService, EncryptService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryManager.Api v1"));
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (Environment.GetEnvironmentVariable("DYNO") == null)
{
    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseCors("SpaPolicy");  

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();

static string GetConnectionString(IConfiguration configuration)
{
    var uriString = Environment.GetEnvironmentVariable("DATABASE_URL")
                        ?? configuration.GetConnectionString("DATABASE_URL");
    var uri = new Uri(uriString!);
    var db = uri.AbsolutePath.Trim('/');
    var user = uri.UserInfo.Split(':')[0];
    var passwd = uri.UserInfo.Split(':')[1];
    var port = uri.Port > 0 ? uri.Port : 5432;
    var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4};SSL Mode=Require;Trust Server Certificate=True;Include Error Detail=True;",
        uri.Host, db, user, passwd, port);
    return connStr;
}
