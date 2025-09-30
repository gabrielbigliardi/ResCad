using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResCad.Application.Interfaces;
using ResCad.Application.Services;
using ResCad.Data.Interfaces;
using ResCad.Data.Mapping;
using ResCad.Dominio.Dtos;
using ResCad.Repository;
using ResCad.Repository.Interfaces;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

/// NO INÍCIO do Program.cs da API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


// CORREÇÃO: Configure o HttpClient DESTA forma


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var url = configuration["Supabase:Url"];
    var key = configuration["Supabase:Key"];

    var options = new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true,
        Schema = "public"
        //ShouldInitializeRealtime = true
    };

    return new Client(url!, key, options);
});

// Dependency Injection
builder.Services.AddScoped<IResidentesRepository, ResidentesRepository>();
builder.Services.AddScoped<IResidentesAplService, ResidentesAplService>();

// Registrar o mapper manual
builder.Services.AddScoped<IRepositorioMap<ResidentesDto, Residentes>, ResidentesMap>();

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

app.UseCors("AllowAll");

app.Run();
