using BookStore.BL.CommandHandlers;
using BookStore.Extentions;
using BookStore.HealthChecks;
using BookStore.Middleware;
using BookStore.Models.MediatR.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddSerilog(logger);

// Add services to the container. 
builder.Services.RegisterRepositories()
                .RegisterServices()
                .AddAutoMapper(typeof(Program));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//health  checks
builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<CustomHealthCheck>("Author Service")
    .AddUrlGroup(new Uri("https://google.bg"), name: "Google Service");

builder.Services.AddMediatR(typeof(GetAllBooksCommandHandler).Assembly);

//App builder below
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
