using ContactManager.Models;
using ContactManager.Services.IServices;
using ContactManager.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        builder.Services.AddDbContext<ManagerContext>(options => options.UseSqlServer(builder.Configuration.
            GetConnectionString("DefaultConnectionString")));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        var host = builder.Configuration.GetSection("CorsHosts").Value;

        app.UseCors(options =>
        {
            options.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });

        app.Run();
    }
}