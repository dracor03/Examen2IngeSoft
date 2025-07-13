using System.Text.Json;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:8080")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddSingleton<Infrastructure.DrinkInventory>();
builder.Services.AddSingleton<CurrencyInventory>();
builder.Services.AddScoped<Application.IGetDrinksQuery, Application.GetDrinksQuery>();
builder.Services.AddScoped<IUpdateDrinkStockCommand, UpdateDrinkStockCommand>();
builder.Services.AddScoped<IDeductCurrencyCommand, DeductCurrencyCommand>();
builder.Services.AddScoped<IProcessPurchaseCommand, ProcessPurchaseCommand>();
builder.Services.AddScoped<IDecreaseStockDrinkCommand, DecreaseStockDrinkCommand>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
