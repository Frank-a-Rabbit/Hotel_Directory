using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using HotelDirectory.Data;
using HotelDirectory.Models;

namespace HotelDirectory
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.WithOrigins("https://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });

                // Configure the database connection and add ApplicationDbContext
                builder.Services.AddDbContext<HotelDirectoryDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelDirectoryDbContext"),
                        sqlOptions => sqlOptions.EnableRetryOnFailure()));

                // Configure Identity
                builder.Services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<HotelDirectoryDbContext>()
                    .AddDefaultTokenProviders();

                var app = builder.Build();

                var userManager = app.Services.GetRequiredService<UserManager<User>>;
                var roleManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>;

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                app.UseCors();

                await app.RunAsync();
        }
    }
}
