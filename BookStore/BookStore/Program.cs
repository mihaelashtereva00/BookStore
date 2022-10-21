using BookStore.BL.CommandHandlers;
using BookStore.Caches.Settings;
using BookStore.DL.Repositories.MsSql;
using BookStore.Extentions;
using BookStore.HealthChecks;
using BookStore.Middleware;
using BookStore.Models.Models.Configurations;
using BookStore.Models.Models.User;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Text;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog(logger);

builder.Services.Configure<MyJsonSettings>(builder.Configuration.GetSection(nameof(MyJsonSettings)));
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(nameof(KafkaSettings)));
builder.Services.Configure<KafkaSettingsProducer>(builder.Configuration.GetSection(nameof(KafkaSettingsProducer)));
builder.Services.Configure<KafkaSettingsConsumer>(builder.Configuration.GetSection(nameof(KafkaSettingsConsumer)));
builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection(nameof(MongoDbConfiguration)));
builder.Services.Configure<HttpClientConfig>(builder.Configuration.GetSection(nameof(HttpClientConfig)));

// Add services to the container. 
builder.Services.RegisterRepositories()
                .RegisterServices()
                .AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put only yout JWT Bearer token in text box below",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    x.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {jwtSecurityScheme, Array.Empty<string>()}
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("View",
        policy => policy.RequireClaim("View"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireClaim("Admin"));
});

//health  checks
builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<CustomHealthCheck>("Author Service")
    .AddUrlGroup(new Uri("https://google.bg"), name: "Google Service");


builder.Services.AddMediatR(typeof(GetAllBooksCommandHandler).Assembly);

builder.Services.AddIdentity<UserInfo, UserRole>()
    .AddUserStore<UserInfoStore>()
    .AddRoleStore<UserRoleStore>();

//App builder below
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapHealthChecks("/health");
app.RegisterHealthChecks();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<CustomMiddleware>();

WebHost.CreateDefaultBuilder(args)
        .UseStartup<Program>()
        .ConfigureLogging((ctx, logging) =>
        {
            logging.AddConfiguration(ctx.Configuration.GetSection("Logging"));
        });


app.Run();
