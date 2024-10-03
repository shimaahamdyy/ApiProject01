using Store.Data.Contexts;
using Store.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Web.Helper;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using Store.Services.Services.ProductServices.Dto;
using Store.Services.Services.ProductServices;
using Store.Web.Middleware;
using Microsoft.AspNetCore.Mvc;
using Store.Services.HandleResponses;
using Store.Web.Extensions;
using System.Windows.Input;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
namespace Store.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllers();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                //var configurations = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                //return ConnectionMultiplexer.Connect(configurations);

                return ConnectionMultiplexer.Connect(ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")));
            });

            builder.Services.AddApplicationServices();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await ApplySeeding.ApplySeedingAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
