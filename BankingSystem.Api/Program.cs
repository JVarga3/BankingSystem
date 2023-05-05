using BankingSystem.Api.Core.Supervisors;
using BankingSystem.Api.Infrastructure.Repositories;
using BankingSystem.Api.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Add serilog
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddSingleton<Dictionary<string, List<Account>>>();
builder.Services.AddScoped<IAccountSupervisor, AccountSupervisor>();
builder.Services.AddScoped<IPaymentSupervisor, PaymentSupervisor>();
builder.Services.AddScoped<IBankRepository, BankRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();