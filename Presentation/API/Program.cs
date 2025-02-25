using Application;
using Domain;
using Persistence;

namespace API;

public static class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Services.AddApiServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddDomainServices();
        builder.Services.AddPersistenceServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(
            options => options
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins(builder.Configuration["AllowedHosts"] ?? "")
        );

        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
    }
}